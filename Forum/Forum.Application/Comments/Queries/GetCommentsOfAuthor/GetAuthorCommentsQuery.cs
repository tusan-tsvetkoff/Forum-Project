using ErrorOr;
using Forum.Contracts.Comment;
using Forum.Contracts.Common;
using MediatR;

namespace Forum.Application.Comments.Queries.GetCommentsOfAuthor;

public record GetAuthorCommentsQuery(
    string AuthorId,
    string? SearchTerm,
    string? SortOrder,
    string? SortColumn,
    string? Username,
    int Page,
    int PageSize) : IRequest<ErrorOr<(List<CommentResult>, PageInfo)>>;
