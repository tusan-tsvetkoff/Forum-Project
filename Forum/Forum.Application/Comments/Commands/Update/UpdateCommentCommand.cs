using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Comments.Commands.Update;

public record UpdateCommentCommand(
    Guid Id,
    Guid PostId,
    Guid UserId,
    string NewContent) : IRequest<ErrorOr<UpdateCommentResult>>; // TODO: Add Change history to the comme
        
