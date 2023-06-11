using ErrorOr;
using Forum.Application.Common.Interfaces.Authentication;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Application.Authentication.Common;
using Forum.Models.Entities;
using MediatR;
using Forum.Data.Common.Errors;

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
        // Check if user already exists - check email
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // Create user (generate unique ID)
        var user = new User
        {
            Email = command.Email,
            Password = command.Password,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Username = command.Username
        };
        _userRepository.Add(user);

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}
