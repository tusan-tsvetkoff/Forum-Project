namespace Forum.Server.Common.Services;

public class SearchService
{
    private string searchQuery = string.Empty;

    public string SearchQuery
    {
        get => searchQuery;
        set => searchQuery = value;
    }
}
