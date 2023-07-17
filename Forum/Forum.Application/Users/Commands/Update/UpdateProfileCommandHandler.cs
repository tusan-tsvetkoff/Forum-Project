using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;
using Forum.Data.Common.Errors;
using Forum.Application.Authentication.Common;

namespace Forum.Application.Users.Commands.Update;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ErrorOr<Updated>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateProfileCommandHandler(IUserRepository userRepository, IAuthorRepository authorRepository, IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
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

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber) && user.IsAdmin)
        {
            user.UpdatePhoneNumber(request.PhoneNumber);
        }

        var author = await _authorRepository.GetByUserIdAsync(userId);

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var hashedPassword = _passwordHasher.HashPassword(request.Password);
            user.UpdatePassword(hashedPassword);
        }

        user.UpdateProfile(
            request.FirstName,
            request.LastName);

        author.UpdateProfile(
            request.FirstName,
            request.LastName,
            request.AvatarUrl);

        await _userRepository.Update(user);

        return Result.Updated;
    }
}
