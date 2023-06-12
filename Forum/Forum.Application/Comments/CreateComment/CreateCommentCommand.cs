using ErrorOr;
using Forum.Data.Comment;
using Forum.Data.Commenter.ValueObjects;
using MediatR;

namespace Forum.Application.Comments.CreateComment;

public record CreateCommentCommand(
    string Content,
    string CommenterId,
    string PostId
    ) : IRequest<ErrorOr<Comment>>;

