using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using MediatR;
using Forum.Data.Common.Errors;
using Forum.Data.UserAggregate.ValueObjects;
using Forum.Data.UserAggregate;

namespace Forum.Application.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(command.UserId);
        // 1. Check if the user exists in the system or not.
        if (await _userRepository.GetUserByIdAsync(userId) is not User user)
        {
            return Errors.User.NotFound;
        }

        // 2. Delete the user from the system.
        await _userRepository.DeleteAsync(user);

        return Result.Deleted;
    }
}
