using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev5.Application.CarOperations.Commands.PatchCar
{
    public class IsActivePatchCommandValidator : AbstractValidator<IsActivePatchCommand>
    {
        public IsActivePatchCommandValidator()
        {
            RuleFor(command => command.carId).GreaterThan(0);
        }
    }
}
