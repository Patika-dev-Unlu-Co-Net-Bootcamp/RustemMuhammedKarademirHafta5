using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Application.BrandOperations.Commands.CreateBrand
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(command => command.Model.BrandName).NotEmpty().MinimumLength(2);//BrandName bos olamaz ve 2 karakterden uzun olmali
        }
    }
}
