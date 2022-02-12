using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Application.CarOperations.Commands.UpdateCar
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            RuleFor(command => command.Model.BrandId).GreaterThan(0);
            RuleFor(command => command.Model.Color).NotEmpty();
            RuleFor(command => command.Model.ModelName).NotEmpty();
            RuleFor(command => command.Model.IsActive).NotEmpty();
        }
    }
}
