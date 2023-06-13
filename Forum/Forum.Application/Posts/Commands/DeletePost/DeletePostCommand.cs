using ErrorOr;
using Forum.Data.PostAggregate;
using MediatR;

namespace Forum.Application.Posts.Commands.DeletePost;

public record DeletePostCommand(
    string UserId,
    string PostId) : IRequest<ErrorOr<Post>>;
