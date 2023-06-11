using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services.Posts
{
    public record PostResult(
            Guid Id,
         string Title,
         string Content,
         Guid UserId,
         List<object> Comments,
         DateTime CreatedDate,
         DateTime LastEditedDate);
        
  
}
