using ErrorOr;
using Forum.Application.Common.Interfaces.Authentication;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Application.Authentication.Common;
using Forum.Data.Common.Errors;
using Forum.Models.Entities;
using MediatR;

namespace Forum.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery querry, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            // 1. Validate existance
            if (_userRepository.GetUserByEmail(querry.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 2. Password is correct?
            if (user.Password != querry.Password)
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
