using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Authentication.Commands.Register;



public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private const int MinNameLength = 4;
    private const int MaxNameLength = 32;

    private const int MinPasswordLength = 15;
    public RegisterCommandValidator()
    {

        RuleFor(x => x.FirstName).NotEmpty()
            .Length(MinNameLength, MaxNameLength);        
        RuleFor(x => x.LastName).NotEmpty()
            .Length(MinNameLength, MaxNameLength);
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Email).NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password).NotEmpty()
            .MinimumLength(MinPasswordLength);
    }
}
