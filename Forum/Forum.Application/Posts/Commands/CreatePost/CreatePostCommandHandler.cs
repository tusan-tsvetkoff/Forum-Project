using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ErrorOr<Post>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CreatePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Post>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(command.UserId, out Guid userId))
        {
            return Errors.Authentication.InvalidGuid;
        }

        var post = Post.Create(
            command.Content,
            command.Title,
            UserId.Create(userId));

        await _postRepository.AddAsync(post);

        return post;
    }
}
