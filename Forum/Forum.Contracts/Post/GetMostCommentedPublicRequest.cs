namespace Forum.Contracts.Post;

public record GetMostCommentedPublicRequest(
    string SortColumn,
    string SortOrder,
    int PageSize,
    int Page);