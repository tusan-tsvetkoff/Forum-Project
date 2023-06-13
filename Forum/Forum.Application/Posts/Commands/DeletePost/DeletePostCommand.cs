using ErrorOr;
using Forum.Data.PostAggregate;
using MediatR;

namespace Forum.Application.Posts.Commands.DeletePost;

public record DeletePostCommand(
    Guid UserId,
    Guid PostId) : IRequest<ErrorOr<Post>>;
