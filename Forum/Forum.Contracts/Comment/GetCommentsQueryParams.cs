namespace Forum.Contracts.Comment;

public record GetCommentsQueryParams(
    string? SearchTerm,
    string? SortOrder,
    string? SortColumn,
    string? Username,
    int? Page,
    int? PageSize);
