using ErrorOr;
using MediatR;

namespace Forum.Application.Users.Commands.Update;

public record UpdateProfileCommand(
    Guid UserId,
    string? FirstName,
    string? LastName,
    string? Password,
    string? AvatarUrl,
    string? PhoneNumber) : IRequest<ErrorOr<Updated>>;
