using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Commands.DeleteBrand;

namespace UnluCo.NetBootcamp.Odev5.UnluCo.NetBootcamp.Odev4.Application.BrandOperations.Commands.DeleteBrand
{
    public class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(command => command.brandId).GreaterThan(0);//BrandId sifirdan buyuk olmali
        }
    }
}
