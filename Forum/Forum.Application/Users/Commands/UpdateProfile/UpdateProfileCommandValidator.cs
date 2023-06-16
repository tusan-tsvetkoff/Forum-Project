using FluentValidation;

namespace Forum.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
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
        private const int MaxAboutLength = 1024;
        private const string AboutLengthErrorMessage = "About must be less than {0} characters long.";

        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop) // Stop validation on first failure
                .Length(MinNameLength, MaxNameLength)
                .WithMessage(string.Format(NameLengthErrorMessage, MinNameLength, MaxNameLength))
                .Matches(NameRegexPattern)
                .WithMessage(NameRequirementsErrorMessage)
                .When(x => !string.IsNullOrEmpty(x.FirstName));

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop) // Stop validation on first failure
                .Length(MinNameLength, MaxNameLength)
                .WithMessage(string.Format(NameLengthErrorMessage, MinNameLength, MaxNameLength))
                .Matches(NameRegexPattern)
                .WithMessage(NameRequirementsErrorMessage)
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop) // Stop validation on first failure
                .Length(MinUsernameLength, MaxUsernameLength)
                .WithMessage(string.Format(UsernameLengthErrorMessage, MinUsernameLength, MaxUsernameLength))
                .When(x => !string.IsNullOrEmpty(x.Username));

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop) // Stop validation on first failure
                .MinimumLength(MinPasswordLength)
                .WithMessage(string.Format(PasswordLengthErrorMessage, MinPasswordLength))
                .Matches(PasswordRegexPattern)
                .WithMessage(PasswordRequirementsErrorMessage)
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.About)
                .Cascade(CascadeMode.Stop) // Stop validation on first failure
                .MaximumLength(MaxAboutLength)
                .WithMessage(string.Format(AboutLengthErrorMessage, MaxAboutLength))
                .When(x => !string.IsNullOrEmpty(x.About));
        }

    }
}
