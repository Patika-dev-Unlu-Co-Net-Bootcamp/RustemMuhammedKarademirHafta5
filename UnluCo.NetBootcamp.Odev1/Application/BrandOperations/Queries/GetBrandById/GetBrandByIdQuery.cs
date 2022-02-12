using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;

namespace UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Queries.GetBrandById
{
    public class GetBrandByIdQuery
    {
        private readonly CarSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        public int brandId { get; set; }
        public GetBrandByIdQuery(CarSystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BrandIdViewModel Handle()
        {
            var brand = _dbContext.Brands.Find(brandId);//CarSystemDbContext altinda tanimli Brands listesinde brandId ile eslesen kayit var mi diye kontrol eder.
            if (brand is null)
                throw new InvalidOperationException("Marka sistemde kayitli degil!");
            BrandIdViewModel brandVM = _mapper.Map<BrandIdViewModel>(brand);//brand nesnesini brandVM ile esler.
            return brandVM;
        }
    }
    public class BrandIdViewModel
    {
        public string BrandName { get; set; }
        public string BrandNameShortening { get; set; }
    }
}
