namespace Forum.Contracts.Post;

public record CreatePostRequest(
    string Title,
    string Content,
    List<string> Tags);