using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Comments.Commands.Delete;

public record DeleteCommentCommand(
    Guid Id,
    Guid PostId,
    Guid UserId) : IRequest<ErrorOr<Deleted>>;