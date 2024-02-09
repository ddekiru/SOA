namespace Application.Authentication.Models
{
    public class AuthenticationResponse
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
