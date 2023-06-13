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
        var post = await _postRepository.GeTByIdAsync(PostId.Create(command.PostId));

        if (post.UserId != UserId.Create(command.UserId))
        {
            return Errors.Authentication.UnauthorizedAction;
        }

        await _postRepository.DeleteAsync(PostId.Create(post.Id.Value));
        return post;
    }
}
