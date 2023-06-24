using ErrorOr;
using Forum.Data.CommentAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Comments.Commands;

public record CreateCommentCommand(
    string Content,
    Guid AuthorId,
    Guid PostId) : IRequest<ErrorOr<(Comment, string AuthorUsername)>>;

