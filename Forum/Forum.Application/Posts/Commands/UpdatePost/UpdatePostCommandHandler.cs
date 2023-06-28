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
        if(post.AuthorId != authorId)
        {
            return Errors.Authentication.UnauthorizedAction;
        }
        // 4. Update post
        if (!string.IsNullOrWhiteSpace(command.NewTitle))
        {
            post.UpdateTitle(command.NewTitle);
        }

        if(!string.IsNullOrWhiteSpace(command.NewContent))
        {
            post.UpdateContent(command.NewContent);
        }

        if(!string.IsNullOrWhiteSpace(command.Tag))
        {
            // 1. Check if tag exists
            if(await _tagRepository.Exists(command.Tag))
            {
                // 2. Get tag
                var tag = await _tagRepository.GetTagByNameAsync(command.Tag);
                // 3. Add tag to post
                post.AddTag(tag!);
            }
            else
            {
                // 4. Create tag
                var tag = Tag.Create(command.Tag);
                // 5. Add tag to database
                await _tagRepository.AddAsync(tag);
                // 6. Add tag to post
                post.AddTag(tag);
            }
        }

        if (!string.IsNullOrWhiteSpace(command.TagToRemove) && post.Tags.Any(t => t.Name == command.TagToRemove))
        {
            // 1. Get tag
            var tag = await _tagRepository.GetTagByNameAsync(command.TagToRemove);
            // 2. Remove tag from post
            post.RemoveTag(tag!);
        }

        await _postRepository.UpdateAsync(post, cancellationToken);
        // 5. Return result
        return Result.Updated;
    }
}
