using Forum.Contracts.Common;

namespace Forum.Contracts.Comment;

public record CommentResponse(
    string Id,
    string PostId,
    string Content,
    string Author,
    string Timestamp);

