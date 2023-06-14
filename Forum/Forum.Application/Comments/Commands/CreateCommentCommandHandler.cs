using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.PostAggregate.Entities;
using MediatR;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Application.Comments.Commands;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ErrorOr<Comment>>
{
    private readonly IPostRepository _postRepository;

    public CreateCommentCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<ErrorOr<Comment>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(PostId.Create(command.PostId));
        if (post == null)
        {
            return Errors.Post.NotFound;
        }

        var comment = Comment.Create(UserId.Create(command.UserId), command.Content);


        return comment;
    }
}
