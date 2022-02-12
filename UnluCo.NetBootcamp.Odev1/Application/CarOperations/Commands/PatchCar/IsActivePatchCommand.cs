using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;
using UnluCo.NetBootcamp.Odev5.Services;

namespace UnluCo.NetBootcamp.Odev5.Application.CarOperations.Commands.PatchCar
{
    public class IsActivePatchCommand
    {
        public IsActiveCarModel Model { get; set; }
        public int carId { get; set; }

        private readonly CarSystemDbContext _dbContext;
        public IsActivePatchCommand(CarSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var car = _dbContext.Cars.Find(carId);
            if (car is null)
                throw new InvalidOperationException("Arac sistemde kayitli degil!");
            car.IsActive = Model.IsActive;
            _dbContext.SaveChanges();
        }

        public class IsActiveCarModel
        {
            public bool IsActive { get; set; } = true;
        }
    }
}
