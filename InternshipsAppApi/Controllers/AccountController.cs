using Application.Authentication;
using Application.Authentication.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternshipsAppApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAdminUserService _adminUserService;
        private readonly IRegularUserService _regularUserService;

        public AccountController(IAuthenticationService authenticationService, IAdminUserService adminUserService, IRegularUserService regularUserService)
        {
            _authenticationService = authenticationService;
            _adminUserService = adminUserService;
            _regularUserService = regularUserService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            try
            {
                var authenticationResponse = await _authenticationService.AuthenticateAsync(request);
                return Ok(authenticationResponse);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (request.IsAdmin)
                    await _adminUserService.CheckFields(request.UserName,
                        request.CompanyName, cancellationToken);
                else
                    await _regularUserService.CheckFields(request.UserName,
                        cancellationToken);

                var registrationResponse = await _authenticationService.RegisterAsync(request);

                if (request.IsAdmin)
                    await _adminUserService
                        .CreateAdminUser(registrationResponse.UserId, request.UserName,
                        request.CompanyName, cancellationToken);
                else
                    await _regularUserService
                        .CreateRegularUser(registrationResponse.UserId, request.UserName,
                        cancellationToken);

                return Ok(registrationResponse);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
