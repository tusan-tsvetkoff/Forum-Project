namespace Forum.Contracts.User;

public record UpdateProfileRequest(
    Guid? UserId,
    string? FirstName,
    string? LastName,
    string? Password,
    string? AvatarUrl,
    string? PhoneNumber);
