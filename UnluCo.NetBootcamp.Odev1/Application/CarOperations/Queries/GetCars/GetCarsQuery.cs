using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;
using UnluCo.NetBootcamp.Odev5.Entities.Concrete;
using UnluCo.NetBootcamp.Odev5.Models;

namespace UnluCo.NetBootcamp.Odev5.Application.CarOperations.Queries.GetCars
{
    public class GetCarsQuery
    {
        private readonly CarSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetCarsQuery(CarSystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<CarsViewModel> Handle()
        {
            var carList = _dbContext.Cars.Include(x => x.Brand).Where(x=>x.IsActive).OrderBy(x => x.Id).ToList();//CarSystemDbContext altinda tanimli Cars listesinde carId ile eslesen ve IsActive degeri true olan kayit var mi diye kontrol eder.
            List<CarsViewModel> viewModelList = _mapper.Map<List<CarsViewModel>>(carList);//carList ile gelen bilgileri viewModelList ile esle. 
            return viewModelList;
        }
    }
    public class CarsViewModel
    {
        public string BrandName { get; set; }//Brands listesi ile eslestirip BrandId ye karsilik gelen BrandName alir
        public string Color { get; set; }
        public string ModelName { get; set; }
        public int ModelYear { get; set; }
    }
}
