using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Lastnames { get; set; } = string.Empty;
    }
}
