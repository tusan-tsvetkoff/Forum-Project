using Forum.Application.Common.Interfaces.Persistence;
using Forum.Application.Common.Interfaces.Services;

namespace Forum.Application.Services;
public class PostStatisticsService : IPostStatisticsService
{
    private readonly IPostRepository _postRepository;

    public PostStatisticsService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<int> GetTotalPostsCountAsync()
    {
        return await _postRepository.GetTotalPostsCountAsync();
    }
}
