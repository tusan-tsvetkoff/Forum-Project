using ErrorOr;
using Forum.Application.Comments.Common;
using Forum.Data.CommentAggregate;
using MediatR;

namespace Forum.Application.Comments.Queries.Get;

public record GetCommentQuery(
    Guid Id,
    Guid PostId) : IRequest<ErrorOr<CommentResult>>;
