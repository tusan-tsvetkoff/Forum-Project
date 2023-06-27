using FluentValidation;

namespace Forum.Application.Posts.Commands.UpdatePost;

public class UpdatePostValidator : AbstractValidator<UpdatePostCommand>
{
    private const int minContentLength = 32;
    private const int maxContentLength = 8192;

    private const string lengthErrorMessage = "New {PropertyName} must be between {MinLength} and {MaxLength} characters long. {PropertyValue} is {TotalLength}";

    private const int minTitleLength = 16;
    private const int maxTitleLength = 64;

    public UpdatePostValidator()
    {
        RuleFor(x => x.NewContent)
            
            .Length(minContentLength, maxContentLength)
            .WithMessage(lengthErrorMessage);

        RuleFor(x => x.NewTitle)
            .Length(minTitleLength, maxTitleLength)
            .WithMessage(lengthErrorMessage);
    }
}
