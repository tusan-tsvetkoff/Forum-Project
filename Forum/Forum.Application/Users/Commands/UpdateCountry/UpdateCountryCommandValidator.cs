using FluentValidation;
using Forum.Application.Authentication.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Users.Commands.UpdateCountry;

public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
{
    public UpdateCountryCommandValidator()
    {
        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Invalid country.");
    }
}
