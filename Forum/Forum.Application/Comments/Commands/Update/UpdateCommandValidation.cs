using FluentValidation;

namespace Forum.Application.Comments.Commands.Update;

public class UpdateCommandValidation : AbstractValidator<UpdateCommentCommand>
{
    private const int MinLength = 1;
    private const int MaxLength = 1000;
    private const string ErrorCode = "ERR_COMMENT_CONTENT_INVALID_LENGTH";
    private const string ErrorMessage = "{Property} must be between {MinLength} and {MaxLength} characters long. You entered {TotalLength}, please try again.";
    private const string OverridePropertyName = "Comment content";

    public UpdateCommandValidation()
    {
        RuleFor(x => x.NewContent)
            .NotEmpty()
            .WithName(OverridePropertyName)
            .Length(MinLength, MaxLength)
            .WithErrorCode(ErrorCode)
            .WithMessage(ErrorMessage);
    }
}
