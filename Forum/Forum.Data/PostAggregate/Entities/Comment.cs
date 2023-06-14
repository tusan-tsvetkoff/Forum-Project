using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Models;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Data.PostAggregate.Entities
{
    public sealed class Comment : Entity<CommentId>
    {
        public CommentId CommentId { get; }
        public string Content { get; private set; }
        public UserId UserId { get; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        public Comment(
            string content,
            UserId userId,
            DateTime createdDateTime)
            : base(id: CommentId.CreateUnique())
        {
            Content = content;
            UserId = userId;
            CreatedDateTime = createdDateTime;
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
    }
}
