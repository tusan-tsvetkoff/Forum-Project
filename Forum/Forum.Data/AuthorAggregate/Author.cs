using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.CommentAggregate.ValueObjects;
using Forum.Data.Models;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Data.AuthorAggregate;

public sealed class Author : AggregateRoot<AuthorId, string>
{
    private readonly List<PostId> _postIds = new();
    private readonly List<CommentId> _commentIds = new();

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Username { get; private set; }
    public UserId UserId { get; private set; }
    public IReadOnlyList<PostId> PostIds => _postIds.AsReadOnly();
    public IReadOnlyList<CommentId> CommentIds => _commentIds.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }

    private Author(
        AuthorId authorId,
        string firstName,
        string lastName,
        string username,
        UserId userId)
        : base(authorId ?? AuthorId.CreateUnique(userId))
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        UserId = userId;
    }

    public static Author Create(
        string firstName,
        string lastName,
        string username,
        UserId userId)
    {
        // TODO: enforce invariants
        return new Author(
            AuthorId.CreateUnique(userId),
            firstName,
            lastName,
            username,
            userId);
    }
}