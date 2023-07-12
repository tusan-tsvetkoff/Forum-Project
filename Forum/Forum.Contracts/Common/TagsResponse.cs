namespace Forum.Contracts.Common;

public record TagList(
    List<TagsResponse> Tags);
public record TagsResponse(
    string Id,
    string Name);
