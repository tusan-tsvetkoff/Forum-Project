using ErrorOr;
using Forum.Application.Comments.Common;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.CommentAggregate.ValueObjects;
using MediatR;
using Forum.Data.Common.Errors;
using Forum.Contracts.Post;

namespace Forum.Application.Comments.Queries.Get;

public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, ErrorOr<CommentResult>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IUserRepository _userRepository;

    public GetCommentQueryHandler(
     ICommentRepository commentRepository,
     IAuthorRepository authorRepository,
     IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _authorRepository = authorRepository;
    }

    public async Task<ErrorOr<CommentResult>> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(CommentId.Create(request.Id));
        if (comment is null)
        {
            return Errors.Comment.NotFound;
        }

        var author = await _authorRepository.GetByAuthorIdAsync(comment.AuthorId)!;
        if (author is null)
        {
            return Errors.Author.NotFound;
        }
        string username = author.Username;
        var user = await _userRepository.GetUserByUsernameAsyc(username);
        var fullName = $"{user.FirstName} {user.LastName}";
        var email = user.Email;
        var authorResponse = new AuthorResponse(author.Id.ToString(), fullName, email, username);


        return new CommentResult(
            comment.Id.Value.ToString(),
            comment.Content,
            authorResponse,
            comment.CreatedAt.ToString(),
            comment?.UpdatedAt.ToString());
    }
}
