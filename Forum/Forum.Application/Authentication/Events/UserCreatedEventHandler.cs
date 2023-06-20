using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;
using Forum.Data.UserAggregate.Events;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Authentication.Events;

public class UserCreatedEventHandler : INotificationHandler<UserCreated>
{
    private readonly IAuthorRepository _authorRepository;

    public UserCreatedEventHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task Handle(UserCreated userCreatedEvent, CancellationToken cancellationToken)
    {
        // 1. Send an email to the user to verify their email address.
        // 2. Send a welcome email to the user.
        // 3. Send a notification to the admin that a new user has registered.
        // 4. Create an author profile for the user.
        var author = Author.Create(
          userId: UserId.Create(userCreatedEvent.User.Id.Value),
          firstName: userCreatedEvent.User.FirstName,
          lastName: userCreatedEvent.User.LastName,
          username: userCreatedEvent.User.Username);
        await _authorRepository.AddАsync(author);
    }
}
