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

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ErrorOr<Deleted>>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;

    public DeletePostCommandHandler(IPostRepository postRepository, IAuthorRepository authorRepository)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeletePostCommand command, CancellationToken cancellationToken)
    {
        var postId = PostId.Create(command.PostId);

        if (!await _postRepository.PostExistsAsync(postId))
        {
            return Errors.Post.NotFound;
        }

        var post = await _postRepository.GetByIdAsync(postId);
        var author = await _authorRepository.GetByUserIdAsync(UserId.Create(command.UserId));

        if (post!.AuthorId != author!.Id)
        {
            return Errors.Authentication.UnauthorizedAction;
        }

        await _postRepository.DeleteAsync(post);

        return Result.Deleted;
    }
}
