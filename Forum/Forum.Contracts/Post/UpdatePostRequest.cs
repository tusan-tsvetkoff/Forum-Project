using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Post;

 public record UpdatePostRequest(
     Guid Id,
     Guid UserId,
     string? NewTitle,
     string? NewContent); // TODO: Also add tags here
