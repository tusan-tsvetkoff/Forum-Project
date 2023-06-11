using Forum.Models.Entities;

namespace Forum.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);