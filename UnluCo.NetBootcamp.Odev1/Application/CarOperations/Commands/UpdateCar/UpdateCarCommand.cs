using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;
using UnluCo.NetBootcamp.Odev5.Services;

namespace UnluCo.NetBootcamp.Odev5.Application.CarOperations.Commands.UpdateCar
{
    public class UpdateCarCommand
    {
        public UpdateCarModel Model { get; set; }
        public int carId { get; set; }

        private readonly CarSystemDbContext _dbContext;
        public UpdateCarCommand(CarSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var car = _dbContext.Cars.Find(carId);
            if (car is null)
                throw new InvalidOperationException("Arac sistemde kayitli degil!");
            car.BrandId = Model.BrandId != default ? Model.BrandId : car.BrandId;
            car.Color = Model.Color != default ? Model.Color : car.Color;
            car.ModelName = Model.ModelName != default ? Model.ModelName : car.ModelName;
            car.IsActive = Model.IsActive != default ? Model.IsActive : car.IsActive;
            _dbContext.SaveChanges();
        }

        public class UpdateCarModel
        {
            public int BrandId { get; set; }
            public string Color { get; set; }
            public string ModelName { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
