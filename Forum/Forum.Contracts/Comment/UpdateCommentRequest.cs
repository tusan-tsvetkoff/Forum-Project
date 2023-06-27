using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Comment;

public record UpdateCommentRequest(
    Guid? Id,
    Guid? PostId,
    Guid? UserId,
    string NewContent);
