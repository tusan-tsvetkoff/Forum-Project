using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.CommentAggregate.Events;
using Forum.Data.CommentAggregate.ValueObjects;
using Forum.Data.Models;
using Forum.Data.PostAggregate.ValueObjects;

namespace Forum.Data.CommentAggregate;

public sealed class Comment : AggregateRoot<CommentId, Guid>
{
    public AuthorId AuthorId { get; private set; } 
    public PostId PostId { get; private set; } 
    public string Content { get; private set; } 
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private Comment()
    {
    }
#pragma warning restore CS8618

    private Comment(
        CommentId commentId,
        AuthorId authorId,
        PostId postId,
        string content,
        DateTime createdAt) : base(id: commentId)
    {
        AuthorId = authorId;
        PostId = postId;
        Content = content;
        CreatedAt = createdAt;
    }

    public static Comment Create(
        AuthorId authorId,
        PostId postId,
        string content)
    {
        var comment = new Comment(
        CommentId.CreateUnique(),
        authorId,
        postId,
        content,
        DateTime.UtcNow);

        comment.AddDomainEvent(new CommentCreated(comment));

        return comment;
    }

    public static void Update(
        Comment comment,
        string content, 
        out string editedTimestamp)
    {
        comment.Content = content;
        comment.UpdatedAt = DateTime.UtcNow;
        editedTimestamp = comment.UpdatedAt?.ToString("dd-MM-yy hh:mm:ss")!;

        comment.AddDomainEvent(new CommentUpdated(comment));
    }
}
