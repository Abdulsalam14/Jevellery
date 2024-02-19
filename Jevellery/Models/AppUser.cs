using Microsoft.AspNetCore.Identity;

namespace Jevellery.Models
{
    public class AppUser:IdentityUser
    {
        public Cart? Cart { get; set; }
    }
}
