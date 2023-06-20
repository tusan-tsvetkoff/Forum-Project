using Forum.Contracts.Comment;

namespace Forum.Contracts.Post;

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

public record Likes(
    int Value);

public record Dislikes(
    int Value);