using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;

namespace UnluCo.NetBootcamp.Odev5.Application.CarOperations.Queries.GetCarById
{
    public class GetCarByIdQuery
    {
        private readonly CarSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        public int carId { get; set; }
        public GetCarByIdQuery(CarSystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public CarIdViewModel Handle()
        {
            var car = _dbContext.Cars.Include(x => x.Brand).Where(x => x.IsActive).SingleOrDefault(x => x.Id == carId);//CarSystemDbContext altinda tanimli Cars listesinde carId ile eslesen ve IsActive degeri true olan kayit var mi diye kontrol eder.
            if (car is null)
                throw new InvalidOperationException("Arac sistemde kayitli degil!");
            CarIdViewModel carVM = _mapper.Map<CarIdViewModel>(car);//car nesnesini carVM ile esler.
            return carVM;
        }
    }
    public class CarIdViewModel
    {
        public string BrandName { get; set; }//Brands listesi ile eslestirip BrandId ye karsilik gelen BrandName alir
        public string Color { get; set; }
        public string ModelName { get; set; }
        public int ModelYear { get; set; }
    }
}
