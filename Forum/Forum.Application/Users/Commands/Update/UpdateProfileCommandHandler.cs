using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;
using Forum.Data.Common.Errors;

namespace Forum.Application.Users.Commands.Update;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ErrorOr<Updated>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;

    public UpdateProfileCommandHandler(IUserRepository userRepository, IAuthorRepository authorRepository)
    {
        _userRepository = userRepository;
        _authorRepository = authorRepository;
    }
    public async Task<ErrorOr<Updated>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(request.UserId);
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user is null)
        {
            return Errors.User.NotFound;
        }

        if(!string.IsNullOrWhiteSpace(request.PhoneNumber) && user.IsAdmin)
        {
            user.UpdatePhoneNumber(request.PhoneNumber);
        }

        var author = await _authorRepository.GetByUserIdAsync(userId);

        if(!string.IsNullOrWhiteSpace(request.Username) && await _userRepository.GetUserByUsernameAsyc(request.Username) is null)
        {
            author!.UpdateUsername(request.Username);
        }
        else
        {
            return Errors.User.UsernameExists;
        }

        author.UpdateProfile(
            request.FirstName,
            request.LastName,
            request.AvatarUrl);


        return Result.Updated;
    }
}
