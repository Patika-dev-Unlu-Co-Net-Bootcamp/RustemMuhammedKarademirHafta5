using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Entities.Concrete
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
