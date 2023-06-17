using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ErrorOr<Post>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;

    public CreatePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository, IAuthorRepository authorRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<Post>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        // Get user who'll become the author of the post
        var userToAuthor = _userRepository.GetUserById(UserId.Create(command.UserId));
        // Assuming the user ALWAYS exists, because the user is authenticated before this command is called
        // TODO: Add error handling for when the user doesn't exist
        // (though this should never happen, because the user is authenticated before this command is called)

        // TODO: Create an author db, and check if author doesn't already exist, if he does, get him DONE
        if (_authorRepository.GetByUserId(userToAuthor.Id.Value) is not Author author)
        {
            // Create author
            author = Author.Create(
            userToAuthor.FirstName,
            userToAuthor.LastName,
            userToAuthor.Username,
            UserId.Create(command.UserId));

            // Add author to db
            _authorRepository.Add(author);
        }


        // Add author to post
        var post = Post.Create(
            command.Content,
            command.Title,
            AuthorId.Create(author.Id.Value));

        await _postRepository.AddAsync(post);

        return post; // TODO: Event handler will publish PostCreatedEvent and add the post to the author's list of posts
    }
}
