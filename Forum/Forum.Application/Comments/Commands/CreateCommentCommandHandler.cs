using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.CommentAggregate;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Comments.Commands;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ErrorOr<(Comment, string AuthorUsername)>>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(IPostRepository postRepository, IAuthorRepository authorRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
        _commentRepository = commentRepository;
    }

    public async Task<ErrorOr<(Comment, string AuthorUsername)>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        // 1. Find the post
        if(!await _postRepository.PostExistsAsync(PostId.Create(command.PostId)))
        {
            return Errors.Post.NotFound;
        }

        // 2. Find the author for the response + post comment creation.
        var author = await _authorRepository.GetByUserIdAsync(UserId.Create(command.AuthorId));
        if(author is null)
        {
            return Errors.User.NotFound;
        }

        // 3. Create the comment
        var comment = Comment.Create(
            AuthorId.Create(author.Id.Value),
            PostId.Create(command.PostId),
            command.Content);

        // 4. Persist comment
        await _commentRepository.AddAsync(comment);

        return (comment, author.Username);
    }
}
