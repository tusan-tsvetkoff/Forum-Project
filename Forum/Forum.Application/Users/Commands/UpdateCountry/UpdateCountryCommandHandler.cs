using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Application.Users.Common;
using Forum.Data.Common.Errors;
using Forum.Data.UserAggregate;
using MediatR;

namespace Forum.Application.Users.Commands.UpdateCountry;

public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, ErrorOr<UserUpdateResult>>
{
    private readonly IUserRepository _userRepository;

    public UpdateCountryCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserUpdateResult>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (_userRepository.GetUserById(request.UserId) is not User user)
        {
            return Errors.User.NotFound;
        }


        _userRepository.Update(user);

        return new UserUpdateResult(user);
    }
}
