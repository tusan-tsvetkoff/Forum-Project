using ErrorOr;
using Forum.Data.PostAggregate.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Comments.Commands;

public record CreateCommentCommand(
    string Content,
    Guid UserId,
    Guid PostId) : IRequest<ErrorOr<Comment>>;

