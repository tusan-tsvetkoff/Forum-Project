using FluentValidation;

namespace Forum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    private const int MinTitleLength = 16;
    private const int MaxTitleLength = 64;

    private const int MinContentLength = 32;
    private const int MaxContentLength = 8192;

    private const string LengthErrorMessage = "{PropertyName} must be between {MinLength} and {MaxLength} characters long";
    private const string RequiredErrorMessage = "{PropertyName} cannot be empty";

    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage(RequiredErrorMessage)
            .Length(MinTitleLength, MaxTitleLength)
            .WithMessage(LengthErrorMessage);
        RuleFor(x => x.Content).NotEmpty().WithMessage(RequiredErrorMessage)
            .Length(MinContentLength, MaxContentLength)
            .WithMessage(LengthErrorMessage);
    }
}
