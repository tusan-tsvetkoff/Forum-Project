using ErrorOr;
using MediatR;

namespace Forum.Application.Posts.Commands.UpdatePost;

public record UpdatePostCommand(
    Guid Id,
    Guid UserId,
    string? NewTitle,
    string? NewContent,
    string? Tag,
    string? TagToRemove) : IRequest<ErrorOr<Updated>>;
