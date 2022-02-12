using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.Entities.Concrete;

namespace UnluCo.NetBootcamp.Odev5.DBOperations
{
    public class CarSystemDbContext : IdentityDbContext<User>
    {
        public CarSystemDbContext(DbContextOptions<CarSystemDbContext> options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
