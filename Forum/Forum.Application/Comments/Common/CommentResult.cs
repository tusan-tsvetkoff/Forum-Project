using Forum.Contracts.Common;
using Forum.Contracts.Post;
using Forum.Data.CommentAggregate;

namespace Forum.Application.Comments.Common;

public record CommentResult(
    string Id,
    string Content,
    AuthorResponse Author,
    string Timestamp,
    string EditedTimestamp);


public record ListCommentResponse(
    string PostId,
    List<CommentResult> Comments,
       PageInfo PageInfo);
