namespace Forum.Contracts.Comment;

public record GetCommentsQueryParams(
    string? SearchTerm,
    string? SortOrder,
    string? SortColumn,
    int? Page,
    int? PageSize);
