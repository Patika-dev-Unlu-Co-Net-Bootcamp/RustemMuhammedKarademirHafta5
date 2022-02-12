using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;

namespace UnluCo.NetBootcamp.Odev5.Application.CarOperations.Commands.DeleteCar
{
    public class DeleteCarCommand
    {
        private readonly CarSystemDbContext _dbContext;
        public int carId { get; set; }
        public DeleteCarCommand(CarSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var car = _dbContext.Cars.Find(carId);
            if (car is null)
                throw new InvalidOperationException("Arac sistemde kayitli degil!");
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
        }
    }
}
