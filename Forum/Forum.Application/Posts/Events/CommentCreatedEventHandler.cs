using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.CommentAggregate.Events;
using Forum.Data.CommentAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Posts.Events;

public class CommentCreatedEventHandler : INotificationHandler<CommentCreated>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;

    public CommentCreatedEventHandler(IPostRepository postRepository, IAuthorRepository authorRepository)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
    }

    public async Task Handle(CommentCreated commentCreatedEvent, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByAuthorIdAsync(commentCreatedEvent.Comment.AuthorId)!;

        var post = await _postRepository.GetByIdAsync(commentCreatedEvent.Comment.PostId);

        if (author is null || post is null)
        {
            throw new InvalidOperationException($"Comment has invalid author id (author id: {commentCreatedEvent.Comment.AuthorId}, post id: {commentCreatedEvent.Comment.PostId}");
        }

        author.AddCommentId(CommentId.Create(commentCreatedEvent.Comment.Id.Value));
        post.AddComment(CommentId.Create(commentCreatedEvent.Comment.Id.Value));

        await _authorRepository.UpdateAsync(author);
        await _postRepository.UpdateAsync(post);
    }
}
