using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Models;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Data.PostAggregate.Entities
{
    public sealed class Comment : Entity<CommentId>
    {
        public CommentId CommentId { get; }
        public string Content { get; }
        public AuthorId AuthorId { get; }
        public DateTime CreatedDateTime { get; private set; }
        public DateTime UpdatedDateTime { get; private set; }

        public Comment(
            string content,
            AuthorId authorId,
            DateTime createdDateTime)
            : base(id: CommentId.CreateUnique())
        {
            Content = content;
            AuthorId = authorId;
            CreatedDateTime = createdDateTime;
        }

        public static Comment Create(
            AuthorId authorId,
            string content)
        {
            return new Comment(
                content,
                authorId,
                DateTime.UtcNow);
        }
    }
}
