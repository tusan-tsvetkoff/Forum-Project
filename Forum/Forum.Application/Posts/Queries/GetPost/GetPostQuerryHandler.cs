using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Contracts.Post;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Posts.Queries.GetPost;

public class GetPostQuerryHandler : IRequestHandler<GetPostQuery, ErrorOr<PostResponse>>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IUserRepository _userRepository;

    public GetPostQuerryHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        IAuthorRepository authorRepository,
        IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<PostResponse>> Handle(GetPostQuery query, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(PostId.Create(query.PostId));

        if (post is null)
        {
            return Errors.Post.NotFound;
        }

        var commentsQuery = await _commentRepository.GetCommentsAsync();
        commentsQuery = commentsQuery.Where(c => c.PostId == post.Id);

        var comments = commentsQuery.ToList();

        var commentResponseList = new List<PostCommentResponse>();

        foreach (var comment in comments)
        {
            var authorId = AuthorId.Create(comment.AuthorId.ToString()!);
            var author = await _authorRepository.GetByAuthorIdAsync(authorId)!;
            var commentResponse = new PostCommentResponse(
                comment.Id.ToString()!,
                comment.Content,
                author!.Username,
                comment.CreatedAt.ToString());

            commentResponseList.Add(commentResponse);
        }

        var postAuthor = await _authorRepository.GetByAuthorIdAsync(post.AuthorId)!;
        var user = await _userRepository.GetUserByUsernameAsyc(postAuthor.Username)!;
        var fullName = user!.FirstName + " " + user.LastName;
        var authorResponse = new AuthorResponse(
                       post.AuthorId.ToString()!,
                       fullName,
                       user.Email,
                       postAuthor!.Username);
        var likes = new LikesResponse(
            post.Likes.Value);

        var dislikes = new DislikesResponse(
                       post.Dislikes.Value);

        var tagList = post.Tags.Select(tag => tag.Name).ToList();

        var postResonse = new PostResponse(
            post.Id.ToString()!,
            authorResponse,
            post.Title,
            post.Content,
            tagList,
            likes,
            dislikes,
            commentResponseList,
            post.CreatedDateTime,
            post.UpdatedDateTime);

        return postResonse;
    }
}
