using Forum.Data.Models;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Data.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
    // TODO: Has to be removed from here, added to Author
    private readonly List<PostId> _postIds = new();
    private readonly List<CommentId> _commentIds = new();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; } // Need to hash

    // TODO: Has to be removed from here, added to Author
    public DateTime CreatedDate { get; private set; }

    // TODO: Has to be removed from here, added to Author (for both)
    public IReadOnlyList<PostId> PostIds => _postIds.AsReadOnly();
    public IReadOnlyList<CommentId> CommentIds => _commentIds.AsReadOnly();

    // TODO: Properties for profile page/update/ (possibly removed and added to Author)
    public string About { get; private set; }

    public User(
        string firstName,
        string lastName,
        string email,
        string username,
        string password,
        DateTime createdDate,
        UserId? userId = null)
        : base(id: userId ?? UserId.CreateUnique())
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
        return new User(
            firstName: firstName,
            lastName: lastName,
            email: email,
            username: username,
            password: password,
            createdDate: DateTime.UtcNow);
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
}
