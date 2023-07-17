using Forum.Contracts.Common;
using Forum.Contracts.Post;

namespace Forum.Contracts.Comment;

public record CommentResponse(
    string Id,
    string PostId,
    string Content,
    string Author,
    string Timestamp);


public record ListCommentResponse(
    string PostId,
    List<CommentResult> Comments,
       PageInfo PageInfo);

public record ListAuthorCommentResponse(
    string AuthorId,
       List<CommentResult> Comments,
       PageInfo PageInfo);

public record CommentResult(
    string Id,
    string Content,
    AuthorResponse Author,
    string Timestamp,
    string EditedTimestamp);