using Microsoft.AspNetCore.Identity;

namespace CinemaOnline.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
