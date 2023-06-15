using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Models;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Data.PostAggregate.Entities;

public sealed class Comment : Entity<CommentId>
{
    public string Content { get; private set; }
    public UserId UserId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }

    private Comment(
        string content,
        UserId userId,
        DateTime createdDateTime,
        DateTime? updatedDateTime = null)
        : base(CommentId.CreateUnique())
    {
        Content = content;
        UserId = userId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Comment Create(
        UserId userId,
        string content)
    {
        return new Comment(
            content,
            userId,
            DateTime.UtcNow);
    }

    public void Edit(string newContent)
    {
        Content = newContent;
    }

#pragma warning disable CS8618
    private Comment()
    {
    }
#pragma warning restore CS8618
}
