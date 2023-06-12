using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Comments
{
    public record CreateCommentRequest(
        string Content,
        string CommenterId,
        string PostId);
}
