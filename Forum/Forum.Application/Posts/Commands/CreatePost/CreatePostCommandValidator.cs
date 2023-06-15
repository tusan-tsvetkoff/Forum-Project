using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty()
            .MinimumLength(16)
            .MaximumLength(64);
        RuleFor(x => x.Content).NotEmpty()
            .MinimumLength(32)
            .MaximumLength(8192);
    }
}
