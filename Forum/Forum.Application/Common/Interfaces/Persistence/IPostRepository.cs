using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;

namespace Forum.Application.Common.Interfaces.Persistence
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        Task DeleteAsync(Post post);
        Task<(List<Post>, int postCount)> GetAllPostsAsync(string? sortBy,
            string? searchTerm,
            AuthorId? authorId,
            int pageCount,
            int pageSize);
        Task<Post?> GetByIdAsync(PostId postId);
        IQueryable<Post> GetPosts();
        Task<List<Post>> ListAsync(AuthorId authorId);
        Task<bool> PostExistsAsync(PostId postId);
        Task UpdateAsync(Post post, CancellationToken cancellationToken);
    }
}
