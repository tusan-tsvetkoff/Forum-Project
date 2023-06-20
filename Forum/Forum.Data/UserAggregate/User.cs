using Forum.Data.Models;
using Forum.Data.UserAggregate.Events;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Data.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; } // <- Hashed (but too lazy to change the name)
    public DateTime CreatedDate { get; private set; }
    public string About { get; private set; }

    private User(
        string firstName,
        string lastName,
        string email,
        string username,
        string password,
        DateTime createdDate,
        UserId userId)
        : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Username = username;
        Password = password;
        CreatedDate = createdDate;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string username,
        string password
        )
    {
        var user = new User(
            userId: UserId.CreateUnique(),
            firstName: firstName,
            lastName: lastName,
            email: email,
            username: username,
            password: password,
            createdDate: DateTime.UtcNow);

        user.AddDomainEvent(new UserCreated(user));

        return user;
    }

    public void UpdateProfile(
        string firstName,
        string lastName,
        string username,
        string password,
        string about)
    {
        if(!string.IsNullOrWhiteSpace(firstName))
        {
            FirstName = firstName;
        }

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            LastName = lastName;
        }

        if (!string.IsNullOrWhiteSpace(username))
        {
            Username = username;
        }

        if (!string.IsNullOrWhiteSpace(password))
        {
            Password = password;
        }

        if (!string.IsNullOrWhiteSpace(about))
        {
            About = about;
        }
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
