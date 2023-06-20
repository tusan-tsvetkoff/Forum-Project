using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.CommentAggregate;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Comments.Commands;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ErrorOr<Comment>>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IUserRepository _userRepository;

    public CreateCommentCommandHandler(IPostRepository postRepository, IUserRepository userRepository, IAuthorRepository authorRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<Comment>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(PostId.Create(command.PostId));

        if (post == null)
        {
            return Errors.Post.NotFound;
        }

        // HAS TO BE A BETTER WAY
        //Author author = null;
        // Find user 
        var userToAuthor = await _userRepository.GetUserByIdAsync(UserId.Create(command.AuthorId)); // should be userId but w/e
        // Take care of potential new author
        if (await _authorRepository.GetByUserIdAsync(UserId.Create(userToAuthor.Id.Value)) is not Author author)
        {
            // Create author
            author = Author.Create(
            userToAuthor.FirstName,
            userToAuthor.LastName,
            userToAuthor.Username,
            UserId.Create(command.AuthorId));
        }

        var comment = Comment.Create(
            AuthorId.Create(author.Id.Value),
            PostId.Create(command.PostId),
            command.Content);

        post.AddComment(comment);

        return comment;
    }
}
