using Forum.Models.Entities;

namespace Forum.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);