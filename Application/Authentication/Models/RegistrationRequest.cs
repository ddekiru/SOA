using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.Models
{
    public class RegistrationRequest
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public bool IsAdmin { get; set; } = false;
        
        public string CompanyName { get; set; }
    }
}
