using Forum.Contracts.Post;

namespace Forum.Application.Comments.Commands.Update;
// Currently not used. Only responding with 204
public record UpdateCommentResult(
    string Id,
     string NewContent,
    string EditedTimestamp,
    AuthorResponse AuthorResponse);



