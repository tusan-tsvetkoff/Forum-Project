using Forum.Contracts.Comment;
using Forum.Contracts.Common;

namespace Forum.Contracts.Post;

public record PostResponse(
     string Id,
     AuthorResponse Author,
     string Title,
     string Content,
     List<string> Tags,
     LikesResponse Likes,
     DislikesResponse Dislikes,
     List<PostCommentResponse> Comments,
     DateTime CreatedDateTime,
     DateTime UpdatedDateTime);

public record LinkResponse(
    string Self,
    string Rel,
    string Method);

public record ListedPostResponse(
    string Id,
    AuthorResponse Author,
    string Title,
    string Content,
    List<string> Tags,
    LikesResponse Likes,
    DislikesResponse Dislikes,
    int Comments,
    string Timestamp,
    string EditedTimestamp);

public record AuthorResponse(
    string Id,
    string? FullName,
    string? Email,
    string Username);

public record LikesResponse(
    int Amount);

public record DislikesResponse(
    int Amount);

public record PostResponseListNew(
    List<ListedPostResponse> Posts,
    PageInfo PageInfo);

public record PostCommentResponse(
    string Id,
    string Content,
    string Author,
    string Timestamp);
