using FluentValidation;
using FluentValidation.Validators;

namespace Forum.Application.Posts.Common;

public class EnumValueValidator<TEnum> : PropertyValidator<Task, TEnum> where TEnum : class
{
    public override string Name => throw new NotImplementedException();

    public override bool IsValid(ValidationContext<Task> context, TEnum value)
    {
        throw new NotImplementedException();
    }
}
