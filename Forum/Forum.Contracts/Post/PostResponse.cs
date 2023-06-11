using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Post
{
    public record PostResponse(
         Guid Id,
         string Title,
         string Content,
         Guid UserId,
         List<object> Comments,
         DateTime CreatedDate,
         DateTime LastEditedDate);

}
