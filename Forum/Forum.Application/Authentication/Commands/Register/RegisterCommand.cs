using ErrorOr;
using Forum.Application.Authentication.Common;
using MediatR;

namespace Forum.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;