using ErrorOr;
using Forum.Application.Common.Interfaces.Authentication;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Application.Authentication.Common;
using Forum.Data.Common.Errors;
using Forum.Data.UserAggregate;
using MediatR;

namespace Forum.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery querry, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            // 1. Validate existance
            if (await _userRepository.GetUserByEmail(querry.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 2. Password is correct?
            if (!_passwordHasher.VerifyPassword(querry.Password, user.Password))
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 3. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
