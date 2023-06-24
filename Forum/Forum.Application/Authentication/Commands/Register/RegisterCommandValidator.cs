using FluentValidation;
using Forum.Application.Common.Interfaces.Persistence;
using System.Runtime.Serialization;

namespace Forum.Application.Authentication.Commands.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private const int MinNameLength = 4;
    private const int MaxNameLength = 32;
    private const string NameLengthErrorMessage = "First and last names must be between {0} and {1} characters long.";

    private const string NameRegexPattern = @"^[A-Z][a-z]+$";
    private const string NameRequirementsErrorMessage = "First and last names must start with an uppercase letter and contain only letters.";

    private const int MinUsernameLength = 3;
    private const int MaxUsernameLength = 32;
    private const string UsernameLengthErrorMessage = "Username must be between {0} and {1} characters long.";


    private const int MinPasswordLength = 8;
    private const string PasswordLengthErrorMessage = "Password must be at least {0} characters long.";

    private const string PasswordRegexPattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[A-Z])(?=.*[a-z]).*$";
    private const string PasswordRequirementsErrorMessage = "Password must contain at least one number, one special symbol, one uppercase letter, and one lowercase letter.";
    public RegisterCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(MinNameLength, MaxNameLength)
            .WithMessage(string.Format(NameLengthErrorMessage, MinNameLength, MaxNameLength))
            .Matches(NameRegexPattern)
            .WithMessage(NameRequirementsErrorMessage);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(MinNameLength, MaxNameLength)
            .WithMessage(string.Format(NameLengthErrorMessage, MinNameLength, MaxNameLength))
            .Matches(NameRegexPattern)
            .WithMessage(NameRequirementsErrorMessage);

        RuleFor(x => x.Username)
            .NotEmpty()
            .Length(MinUsernameLength, MaxUsernameLength)
            .WithMessage(string.Format(UsernameLengthErrorMessage, MinUsernameLength, MaxUsernameLength));

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Email)
            .MustAsync(async (email, cancellationToken) =>
            {
                return await userRepository.IsEmailUniqueAsync(email);
            })
            .WithMessage("Email is already in use.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(MinPasswordLength)
            .WithMessage(string.Format(PasswordLengthErrorMessage, MinPasswordLength))
            .Matches(PasswordRegexPattern)
            .WithMessage(PasswordRequirementsErrorMessage);
    }
}
