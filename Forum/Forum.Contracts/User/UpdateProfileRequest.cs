namespace Forum.Contracts.User;

public record UpdateProfileRequest(
    Guid UserId,
    string? FirstName,
    string? LastName,
    string? Username,
    string? AvatarUrl,
    string? PhoneNumber);
