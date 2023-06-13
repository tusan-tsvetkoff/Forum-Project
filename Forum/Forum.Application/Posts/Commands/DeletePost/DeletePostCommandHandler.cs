using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
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
    private readonly IUserRepository _userRepository;

    public DeletePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Post>> Handle(DeletePostCommand command, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(command.UserId, out Guid userId))
        {
            return Errors.Authentication.InvalidGuid;
        }

        if(!Guid.TryParse(command.PostId, out Guid postId))
        {
            return Errors.Authentication.InvalidGuid;
        }

        var post = await _postRepository.GeTByIdAsync(PostId.Create(postId));

        // Ensure that only the author of the post can delete it
        if (post.UserId != UserId.Create(userId))
        {
            return Errors.Authentication.UnauthorizedAction;
        }

        await _postRepository.DeleteAsync(PostId.Create(post.Id.Value));

        return post;
    }
}
