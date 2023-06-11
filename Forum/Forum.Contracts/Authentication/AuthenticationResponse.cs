using System;

namespace Forum.Contracts.Authentication;
public record AuthenticationResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Username,
        string Email,
        string Token);
