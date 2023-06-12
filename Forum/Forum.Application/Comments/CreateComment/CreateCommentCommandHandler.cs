using ErrorOr;
using Forum.Data.Comment;
using Forum.Data.Commenter.ValueObjects;
using MediatR;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.Design;

namespace Forum.Application.Comments.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ErrorOr<Comment>>
    {
        public Task<ErrorOr<Comment>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            // Create Comment
            var comment = Comment.Create(
                request.Content,
                request.PostId,
                CommenterId.CreateUnique());
            // Persist Comment



            // Return Comment
            return default!;
        }
    }
}
