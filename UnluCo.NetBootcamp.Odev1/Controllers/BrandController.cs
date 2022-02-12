using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.DBOperations;
using UnluCo.NetBootcamp.Odev5.Entities.Concrete;
using UnluCo.NetBootcamp.Odev5.Services;
using static UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Commands.CreateBrand.CreateBrandCommand;
using static UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Commands.UpdateBrand.UpdateBrandCommand;
using UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Commands.CreateBrand;
using UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Commands.DeleteBrand;
using UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Commands.UpdateBrand;
using UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Queries.GetBrandById;
using UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Queries.GetBrands;
using UnluCo.NetBootcamp.Odev5.UnluCo.NetBootcamp.Odev4.Application.BrandOperations.Commands.DeleteBrand;
using Microsoft.AspNetCore.Authorization;

namespace UnluCo.NetBootcamp.Odev5.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize]
    public class BrandController : ControllerBase
    {
        private readonly CarSystemDbContext _context;
        private readonly IMapper _mapper;

        public BrandController(CarSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            GetBrandsQuery query = new GetBrandsQuery(_context,_mapper);
            var brandsList = query.Handle();
            return Ok(brandsList);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBrandByIdQuery query = new GetBrandByIdQuery(_context,_mapper);
            query.brandId = id;
            GetBrandByIdQueryValidator validator = new GetBrandByIdQueryValidator();
            validator.ValidateAndThrow(query);
            var brand = query.Handle();
            return Ok(brand);
        }
        //marka isimlerini a-z sirada getirir
        [HttpGet("listBrandNameAsc")]
        public IActionResult GetListBrandNameAsc()
        {
            GetBrandsQuery query = new GetBrandsQuery(_context,_mapper);
            var brandsList = query.Handle().OrderBy(x => x.BrandName).ToList();
            return Ok(brandsList);
        }
        //marka isimlerini z-a sirada getirir
        [HttpGet("listBrandNameDesc")]
        public IActionResult GetListBrandNameDesc()
        {
            GetBrandsQuery query = new GetBrandsQuery(_context, _mapper);
            var brandsList = query.Handle().OrderByDescending(x => x.BrandName).ToList();
            return Ok(brandsList);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateBrandModel brand)
        {
            if (ModelState.IsValid)
            {
            CreateBrandCommand command = new CreateBrandCommand(_context,_mapper);
            command.Model = brand;
            CreateBrandCommandValidator validator = new CreateBrandCommandValidator();
            validator.ValidateAndThrow(command);//Fluentvalidation kutuphanesinden kontrol edip varsa hata firlatan bir fonksiyon
            command.Handle();
            return StatusCode(201);
            }
            else
            {
                //throw new InvalidOperationException("Yetkili kullanici degil!");
                return StatusCode(401);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UpdateBrandModel brand)
        {
            UpdateBrandCommand command = new UpdateBrandCommand(_context);
            command.brandId = id;
            command.Model = brand;
            UpdateBrandCommandValidator validator = new UpdateBrandCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
            DeleteBrandCommand command = new DeleteBrandCommand(_context);
            command.brandId = id;
            DeleteBrandCommandValidator validator = new DeleteBrandCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
            }
            else
            {
                return StatusCode(401);
            }
        }
    }
}
