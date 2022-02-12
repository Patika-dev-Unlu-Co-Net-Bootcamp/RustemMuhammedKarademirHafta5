using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev5.Application.CarOperations.Queries.GetCarById;

namespace UnluCo.NetBootcamp.Odev5.Application.CarOperations.Queries.GetCarById
{
    public class GetCarByIdQueryValidator : AbstractValidator<GetCarByIdQuery>
    {
        public GetCarByIdQueryValidator()
        {
            RuleFor(command => command.carId).GreaterThan(0);//CarId sifirdan buyuk olmali
        }
    }
}
