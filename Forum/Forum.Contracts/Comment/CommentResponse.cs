namespace Forum.Contracts.Comment;

public record CommentResponse(
    string UserId,
    string CommentId,
    string Content,
    DateTime CreatedDateTime);
