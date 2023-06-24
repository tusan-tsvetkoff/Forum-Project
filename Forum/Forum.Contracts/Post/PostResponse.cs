using Forum.Contracts.Comment;
using Forum.Contracts.Common;

namespace Forum.Contracts.Post;

public record PostResponse(
     string Id,
     string AuthorId,
     string Title,
     string Content,
     DateTime CreatedDateTime,
     DateTime UpdatedDateTime,
     Likes Likes,
     Dislikes Dislikes,
     List<CommentResponse> Comments);

public record Likes(
    int Value);

public record Dislikes(
    int Value);

public record PostResponseList(
    List<PostResponse> Posts,
       int TotalPosts);

public record PostResponseListNew(
    List<PostResponse> Posts,
    PageInfo PageInfo);