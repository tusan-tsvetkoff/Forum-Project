using ErrorOr;
using MediatR;

namespace Forum.Application.Posts.Commands.UpdatePost;

public record UpdatePostCommand(
    Guid Id,
    Guid UserId,
    string? NewTitle,
    string? NewContent) : IRequest<ErrorOr<Updated>>;
