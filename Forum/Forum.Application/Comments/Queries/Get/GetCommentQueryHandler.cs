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

    public GetCommentQueryHandler(
     ICommentRepository commentRepository,
     IAuthorRepository authorRepository)
    {
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

        return new CommentResult(
            comment.Id.Value.ToString(),
            comment.Content,
            new AuthorResponse(comment.Id.Value.ToString(),username),
            comment.CreatedAt.ToString(),
            comment?.UpdatedAt.ToString());
    }
}
