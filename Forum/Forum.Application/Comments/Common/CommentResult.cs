using Forum.Contracts.Common;
using Forum.Contracts.Post;

namespace Forum.Application.Comments.Common;

public record CommentResult(
    string Id,
    string Content,
    AuthorResponse Author,
    string Timestamp,
    string EditedTimestamp);
