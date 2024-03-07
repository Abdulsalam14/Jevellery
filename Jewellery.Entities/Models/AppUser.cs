using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Entities.Models
{
    public class AppUser : IdentityUser
    {
        public Cart? Cart { get; set; }
    }
}
