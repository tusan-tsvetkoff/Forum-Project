using ErrorOr;
using Forum.Application.Authentication.Common;
using Forum.Application.Common.Interfaces.Authentication;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.Common.Errors;
using Forum.Data.UserAggregate;
using MediatR;

namespace Forum.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // 1. Checking if the email is unique in the system or not.
        /*if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }*/

        // 2.Creating a user (generating a unique ID) & Persisting to DB(in-memory for now).
        // 2.1 Hash the password before persisting to DB.
        var hashedPassword = _passwordHasher.HashPassword(command.Password);

        var user = User.Create(
            firstName: command.FirstName,
            lastName: command.LastName,
            email: command.Email,
            username: command.Username,
            password: hashedPassword
            );

        await _userRepository.AddAsync(user);

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}
