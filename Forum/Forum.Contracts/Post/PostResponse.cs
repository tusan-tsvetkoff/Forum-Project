using Forum.Contracts.Comment;
using Forum.Contracts.Common;

namespace Forum.Contracts.Post;

public record PostResponse(
     string Id,
     string AuthorId,
     string Title,
     string Content,
     List<string> Tags,
     Likes Likes,
     Dislikes Dislikes,
     List<PostCommentResponse> Comments,
     DateTime CreatedDateTime,
     DateTime UpdatedDateTime);

public record ListedPostResponse(
    string Id,
    AuthorResponse Author,
    string Title,
    string Content,
    List<string> Tags,
    Likes Likes,
    Dislikes Dislikes,
    int Comments,
    string Timestamp,
    string EditedTimestamp);

public record AuthorResponse(
    string Id,
    string Username);

public record Likes(
    int Amount);

public record Dislikes(
    int Amount);

public record PostResponseListNew(
    List<ListedPostResponse> Posts,
    PageInfo PageInfo);

public record PostCommentResponse(
    string Id,
    string Content,
    string Author,
    string Timestamp);

