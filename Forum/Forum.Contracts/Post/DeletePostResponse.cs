namespace Forum.Contracts.Post;

public record DeletePostResponse
{
    public string Message { get; } = "Post deleted.";

    public DeletePostResponse(string message)
    {
        Message = message;
    }
}
