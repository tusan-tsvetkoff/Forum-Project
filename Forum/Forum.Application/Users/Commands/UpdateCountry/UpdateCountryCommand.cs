using ErrorOr;
using Forum.Application.Users.Common;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Users.Commands.UpdateCountry;

public record UpdateCountryCommand(
    UserId UserId,
    string Country) : IRequest<ErrorOr<UserUpdateResult>>;
