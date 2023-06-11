using Forum.Application.Common.Interfaces.Authentication;

namespace Forum.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGeneratorl)
    {
        this._jwtTokenGenerator = jwtTokenGeneratorl;
    }

    public AuthenticationResult Register(string firstName, string lastName, string username, string email, string password)
    {
        // Check if user already exists - check email

        // Create user (generate unique ID)

        // Create JWT Token
        Guid userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

        return new AuthenticationResult(
            userId,
            firstName,
            lastName,
            username,
            email,
            token);
    }
    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            "firstname",
            "lastname",
            "username",
            email,
            password);
    }

}
