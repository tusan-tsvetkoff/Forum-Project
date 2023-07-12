using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;
using Forum.Data.Common.Errors;
using Forum.Data.TagEntity;

namespace Forum.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ErrorOr<Post>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ITagRepository _tagRepository;

    public CreatePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository, IAuthorRepository authorRepository, ITagRepository tagRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
        _tagRepository = tagRepository;
    }

    public async Task<ErrorOr<Post>> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
         // Get the author profile of the user
         var userToAuthor = await _userRepository.GetUserByIdAsync(UserId.Create(command.UserId));

        // Get the author profile of the user
        var author = await _authorRepository.GetByUserIdAsync(UserId.Create(userToAuthor!.Id.Value));

        // Create the post with the author's ID
        var post = Post.Create(
            command.Title,
            command.Content,
            AuthorId.Create(author!.Id.Value));
        await _postRepository.AddAsync(post);

        // Add the tags to the post
        foreach (var tag in command.Tags)
        {
            var tagEntity = await _tagRepository.GetTagByNameAsync(tag);
            post.AddTag(tagEntity!);
        }

        return post;
    }
}
