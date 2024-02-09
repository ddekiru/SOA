using Microsoft.AspNetCore.Identity;

namespace Authentication
{
    public class AppUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
