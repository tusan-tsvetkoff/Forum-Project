using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.TagEntity;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ErrorOr<Updated>>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ITagRepository _tagRepository;

    public UpdatePostCommandHandler(IPostRepository postRepository, IAuthorRepository authorRepository, ITagRepository tagRepository)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
        _tagRepository = tagRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
    {
        var postId = PostId.Create(command.Id);
        // 1. Check if post exists
        var post = await _postRepository.GetByIdAsync(postId);
        if (post is null)
        {
            return Errors.Post.NotFound;
        }
        // 2. Check if author exists
        var author = await _authorRepository.GetByUserIdAsync(UserId.Create(command.UserId));
        if (author is null)
        {
            return Errors.Author.NotFound;
        }
        var authorId = AuthorId.Create(author.Id.Value);
        // 3. Check if author is owner of the post
        if (post.AuthorId != authorId)
        {
            return Errors.Authentication.UnauthorizedAction;
        }
        // 4. Update post
        // 4.1. Title updating is deprecated for now
        if (!string.IsNullOrWhiteSpace(command.NewTitle))
        {
            post.UpdateTitle(command.NewTitle);
        }
        if (!string.IsNullOrWhiteSpace(command.NewContent))
        {
            post.UpdateContent(command.NewContent);
        }

        if (command.Tag is not null)
        {
            foreach (var tag in command.Tag)
            {
                // 1. Get tag
                var tagEntity = await _tagRepository.GetTagByNameAsync(tag);
                // 2. Add tag to post
                post.AddTag(tagEntity!);
            }
        }

        if (command.TagToRemove is not null)
        {
            foreach (var tag in command.TagToRemove)
            {
                // 1. Get tag
                var tagEntity = await _tagRepository.GetTagByNameAsync(tag);
                // 2. Remove tag from post
                post.RemoveTag(tagEntity);
            }
        }

        await _postRepository.UpdateAsync(post, cancellationToken);
        // 5. Return result
        return Result.Updated;
    }
}
