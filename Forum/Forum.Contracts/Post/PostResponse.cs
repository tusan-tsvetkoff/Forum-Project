using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Post
{
    public record PostResponse(
         string Id,
         string AuthorId,
         string Title,
         string Content,
         DateTime CreatedDateTime,
         DateTime UpdatedDateTime,
         Likes Likes,
         Likes Dislikes,
         List<CommentResponse> Comments);


    public record CommentResponse(
        string Id,
        string AuthorId,
        string Content,
        DateTime CreatedDateTime);

    public record Likes(
        int Value);

    public record Dislikes(
        int Value);


}
