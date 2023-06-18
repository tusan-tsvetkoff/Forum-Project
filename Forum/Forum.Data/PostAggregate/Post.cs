using ErrorOr;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.CommentAggregate;
using Forum.Data.CommentAggregate.ValueObjects;
using Forum.Data.Models;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.TagAggregate.ValueObjects;

namespace Forum.Data.PostAggregate;

public sealed class Post : AggregateRoot<PostId, Guid>
{
    private readonly List<CommentId> _commentIds = new();
    private readonly List<TagId> _tagIds = new();
    public string Title { get; private set; }
    public string Content { get; private set; }
    public IReadOnlyList<CommentId> CommentIds => _commentIds.AsReadOnly();
    public IReadOnlyList<TagId> TagIds => _tagIds.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    public Likes Likes { get; private set; }
    public Dislikes Dislikes { get; private set; }
    public AuthorId AuthorId { get; private set; }

#pragma warning disable CS8618
    private Post()
    {
    }
#pragma warning restore CS8618

    private Post(
        PostId postId,
        string title,
        string content,
        DateTime createdDateTime,
        AuthorId authorId)
        : base(postId)
    {
        Title = title;
        Content = content;
        CreatedDateTime = createdDateTime;
        AuthorId = authorId;
    }

    public static Post Create(
        string title,
        string content,
        AuthorId authorId)
    {
        // enforce invariants later
        var post = new Post(
            PostId.CreateUnique(),
            title,
            content,
            DateTime.UtcNow,
            authorId
            );
        return post;
    }

    public ErrorOr<Success> AddComment(Comment comment)
    {
        //_comments.Add(comment);
        return Result.Success;
    }

    public void IncrementLikes()
    {
        Likes = Likes.Increment();
    }

    public void IncrementDislikes()
    {
        Dislikes = Dislikes.Increment();
    }
}
