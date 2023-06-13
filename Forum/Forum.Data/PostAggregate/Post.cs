﻿using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Models;
using Forum.Data.PostAggregate.Entities;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Data.PostAggregate;

public sealed class Post : AggregateRoot<PostId, Guid>
{
    private readonly List<Comment> _comments = new();

    public string Title { get; private set; }
    public string Content { get; private set; }
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    public Likes Likes { get; private set; }
    public Dislikes Dislikes { get; private set; }
    public UserId UserId { get; private set; }
    //public AuthorId AuthorId { get; private set; }


    // REFACTORING: Replace UserId userId with AuthorId authorId later if fuck up
    public Post(
        PostId postId,
        string title,
        string content,
        DateTime createdDateTime,
        UserId userId)
        : base(id: postId)
    {
        Title = title;
        Content = content;
        CreatedDateTime = createdDateTime;
        UserId = userId;
        //AuthorId = authorId;
    }

    public static Post Create(
        string title,
        string content,
        UserId userId)
    {
         // REFACTORING: Replace UserId userId with AuthorId authorId later if fuck up
        // enforce invariants later
        var post = new Post(
            PostId.CreateUnique(),
            title,
            content,
            DateTime.UtcNow,
            userId
            );
        return post;
    }

    public void AddComment(string content, AuthorId authorId)
    {
        var comment = Comment.Create(authorId, content);
        _comments.Add(comment);
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
