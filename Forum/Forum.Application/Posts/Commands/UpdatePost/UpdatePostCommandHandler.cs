using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ErrorOr<Updated>>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;

    public UpdatePostCommandHandler(IPostRepository postRepository, IAuthorRepository authorRepository)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var postId = PostId.Create(request.Id);
        // 1. Check if post exists
        var post = await _postRepository.GetByIdAsync(postId);
        if (post is null)
        {
            return Errors.Post.NotFound;
        }
        // 2. Check if author exists
        var author = await _authorRepository.GetByUserIdAsync(UserId.Create(request.UserId));
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
        if (!string.IsNullOrWhiteSpace(request.NewTitle))
        {
            post.UpdateTitle(request.NewTitle);
        }

        if(!string.IsNullOrWhiteSpace(request.NewContent))
        {
            post.UpdateContent(request.NewContent);
        }
        
        await _postRepository.UpdateAsync(post, cancellationToken);
        // 5. Return result
        return Result.Updated;
    }
}
