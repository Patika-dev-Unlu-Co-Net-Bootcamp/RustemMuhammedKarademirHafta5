using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;

namespace UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Queries.GetBrands
{
    public class GetBrandsQuery
    {
        private readonly CarSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBrandsQuery(CarSystemDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BrandsViewModel> Handle()
        {
            var brandList = _dbContext.Brands.OrderBy(x => x.Id).ToList();
            List<BrandsViewModel> viewModelList = _mapper.Map<List<BrandsViewModel>>(brandList);//brandList ile gelen bilgileri viewModelList ile esle. 
            return viewModelList;
        }
    }
    public class BrandsViewModel
    {
        public string BrandName { get; set; }
        public string BrandNameShortening { get; set; }
    }
}
