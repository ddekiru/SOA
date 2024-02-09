using Application.Authentication;
using Application.Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationService(IOptions<JwtSettings> jwtSettings,
            SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) throw new ArgumentException("Invalid login data");
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if (!result.Succeeded) throw new ArgumentException("Invalid login data");

            var jwtSecurityToken = await GenerateToken(user);

            var response = new AuthenticationResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName,
                IsAdmin = user.IsAdmin
            };
            return response;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
                throw new ArgumentException($"The username '{request.UserName}' is already used");
            if (request.Password.Length < 6)
                throw new ArgumentException("At least 6 characters for password");

            var user = new AppUser
            {
                UserName = request.UserName,
                IsAdmin = request.IsAdmin
            };

            if (!await _roleManager.RoleExistsAsync(Constants.Roles.ADMINUSER))
                await _roleManager.CreateAsync(new IdentityRole(Constants.Roles.ADMINUSER));
            if (!await _roleManager.RoleExistsAsync(Constants.Roles.REGULARUSER))
                await _roleManager.CreateAsync(new IdentityRole(Constants.Roles.REGULARUSER));

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if (request.IsAdmin)
                    await _userManager.AddToRoleAsync(user, Constants.Roles.ADMINUSER);
                if (!request.IsAdmin)
                    await _userManager.AddToRoleAsync(user, Constants.Roles.REGULARUSER);
                return new RegistrationResponse { UserId = user.Id };
            }
            throw new ArgumentException($"{result.Errors}");
        }

        private async Task<JwtSecurityToken> GenerateToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(t => new Claim("roles", t)).ToList();

            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("uid", user.Id)
                }
                .Union(userClaims)
                .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.DurationInDays),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
