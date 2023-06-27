using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Application.Posts.Queries.ListPosts;
using Forum.Contracts.Post;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.CommentAggregate;
using Forum.Data.CommentAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Comments.Commands.Update;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, ErrorOr<UpdateCommentResult>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPostRepository _postRepository;

    public UpdateCommentCommandHandler(ICommentRepository commentRepository, IAuthorRepository authorRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _authorRepository = authorRepository;
        _postRepository = postRepository;
    }
    public async Task<ErrorOr<UpdateCommentResult>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        // 1. Check if comment || post exists
        var commentExists = await _commentRepository.CommentExistsAsync(CommentId.Create(request.Id));
        if (!commentExists)
        {
            return Errors.Comment.NotFound;
        }
        var postExists = await _postRepository.PostExistsAsync(PostId.Create(request.PostId));

        if (!postExists)
        {
            return Errors.Post.NotFound;
        }

        // 2. Check if author exists
        var userId = UserId.Create(request.UserId);
        var author = await _authorRepository.GetByUserIdAsync(userId);
        if (author is null)
        {
              return Errors.Author.NotFound;
        }
        var authorId = AuthorId.Create(author.Id.Value);
        // 3. Check if author is owner of the comment
        var comment = await _commentRepository.GetByIdAsync(CommentId.Create(request.Id));
        if (comment.AuthorId != authorId)
        {
            return Errors.Comment.NotOwner;
        }
        // 4. Update Comment
        Comment.Update(comment, request.NewContent, out string editedTimestamp);

        // 5. Update changes in the database
        await _commentRepository.UpdateAsync(comment, cancellationToken);

        // 6. Return result
        return new UpdateCommentResult(
            comment.Id.Value.ToString(),
            request.NewContent,
            editedTimestamp,
            new AuthorResponse(authorId.Value, author.Username));
    }
}
