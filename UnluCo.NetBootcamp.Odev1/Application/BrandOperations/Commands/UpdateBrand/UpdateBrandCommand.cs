using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;
using UnluCo.NetBootcamp.Odev5.Services;

namespace UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Commands.UpdateBrand
{
    public class UpdateBrandCommand
    {
        public UpdateBrandModel Model { get; set; }
        public int brandId { get; set; }

        private readonly CarSystemDbContext _dbContext;
        public UpdateBrandCommand(CarSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var brand = _dbContext.Brands.Find(brandId);
            if (brand is null)
                throw new InvalidOperationException("Marka sistemde kayitli degil!");
            brand.BrandName = Model.BrandName != default ? Model.BrandName : brand.BrandName;
            _dbContext.SaveChanges();
        }

        public class UpdateBrandModel
        {
            public string BrandName { get; set; }
        }
    }
}
