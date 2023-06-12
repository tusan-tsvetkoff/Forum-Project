using ErrorOr;
using Forum.Application.Authentication.Common;
using Forum.Application.Common.Interfaces.Authentication;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.Common.Errors;
using MediatR;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // 1. Checking if the email is unique in the system or not.
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // 2.Creating a user (generating a unique ID) & Persisting to DB(in-memory for now).

        var user = User.Create(
            firstName: command.FirstName,
            lastName: command.LastName,
            email: command.Email,
            username: command.Username,
            password: command.Password
            );
        _userRepository.Add(user);

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}
