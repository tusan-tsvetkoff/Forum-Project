using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Comment;

public record GetCommentRequest(
    Guid Id,
    Guid PostId);