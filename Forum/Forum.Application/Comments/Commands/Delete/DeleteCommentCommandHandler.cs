using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.CommentAggregate.ValueObjects;
using MediatR;
using Forum.Data.Common.Errors;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Application.Comments.Commands.Delete;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, ErrorOr<Deleted>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPostRepository _postRepository;
    public DeleteCommentCommandHandler(
            ICommentRepository commentRepository, IPostRepository postRepository, IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        if(await _postRepository.PostExistsAsync(PostId.Create(command.PostId)) is false)
        {
            return Errors.Post.NotFound;
        }

        var commentToDelete = await _commentRepository.GetByIdAsync(CommentId.Create(command.Id));
        if (commentToDelete is null)
        {
            return Errors.Comment.NotFound;
        }

        var author = await _authorRepository.GetByUserIdAsync(UserId.Create(command.UserId));
        if (author is null)
        {
            return Errors.Author.NotFound;
        }

        // Check if requester is the owner of the comment
        if(commentToDelete.AuthorId != author.Id)
        {
            return Errors.Comment.NotOwner;
        }

        await _commentRepository.DeleteAsync(commentToDelete);
        
        return Result.Deleted;
    }
}
