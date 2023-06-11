using Forum.Application.Common.Interfaces.Authentication;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Models.Entities;

namespace Forum.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGeneratorl, IUserRepository userRepository)
    {
        this._jwtTokenGenerator = jwtTokenGeneratorl;
        this._userRepository = userRepository;
    }

    public AuthenticationResult Register(string firstName, string lastName, string username, string email, string password)
    {
        // Check if user already exists - check email
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User with given email already exists.");
        }


        // Create user (generate unique ID)
        var user = new User
        {
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
            Username = username
        };
        _userRepository.Add(user);

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
    public AuthenticationResult Login(string email, string password)
    {
        // 1. Validate existance
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email does not exist.");
        }

        // 2. Password is correct?
        if (user.Password != password)
        {
            throw new Exception("Invalid credentials.");
        }

        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

}
