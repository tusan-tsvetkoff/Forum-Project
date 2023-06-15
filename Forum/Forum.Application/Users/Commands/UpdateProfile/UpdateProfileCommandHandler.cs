using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ErrorOr<Updated>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateProfileCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<Updated>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserById(UserId.Create(command.UserId)) is not User user)
            {
                return Error.NotFound("User not found");
            }

            user.UpdateProfile(
                firstName: command.FirstName,
                lastName: command.LastName,
                username: command.Username,
                password: command.Password,
                about: command.About);

            _userRepository.Update(user);

            return new Updated();
        }
    }
}
