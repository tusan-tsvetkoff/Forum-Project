using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;
using System.Runtime.CompilerServices;

namespace Forum.Application.Posts.Commands.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ErrorOr<Post>>
{
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<ErrorOr<Post>> Handle(DeletePostCommand command, CancellationToken cancellationToken)
    {
        if (await _postRepository.PostExistsAsync(PostId.Create(command.PostId)))
        {
            return Errors.Post.NotFound;
        }

        var post = await _postRepository.GetByIdAsync(PostId.Create(command.PostId));

        if (post!.AuthorId != AuthorId.Create(UserId.Create(command.UserId).Value.ToString()))
        {
            return Errors.Authentication.UnauthorizedAction;
        }

        await _postRepository.DeleteAsync(PostId.Create(post.Id.Value));

        return post;
    }
}
