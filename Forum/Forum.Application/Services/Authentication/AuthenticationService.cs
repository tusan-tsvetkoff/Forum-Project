namespace Forum.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "firstname","lastname","username", email, password);
    }

    public AuthenticationResult Register(string firstName, string lastName, string username, string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            firstName,
            lastName,
            username,
            email,
            password);
    }
}
