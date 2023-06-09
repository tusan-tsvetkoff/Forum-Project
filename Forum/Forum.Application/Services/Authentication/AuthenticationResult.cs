namespace Forum.Application.Services.Authentication;

public record AuthenticationResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Token
);