using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ForumDbContext _dbContext;
        public PostRepository(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Post post)
        {
            _dbContext.Add(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Post?> GetByIdAsync(PostId postId)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<List<Post>> ListAsync(AuthorId authorId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PostExistsAsync(PostId postId)
        {
            return await _dbContext.Posts.AnyAsync(p => p.Id == postId);
        }

        public async Task DeleteAsync(PostId postId)
        {
            _dbContext.Posts.Remove(await GetByIdAsync(postId));
        }

        public IQueryable<Post> GetPosts()
        {
            return _dbContext.Posts.AsQueryable();
        }

        public async Task<(List<Post>, int postCount)> GetAllPostsAsync(
            string? sortBy,
            string? searchTerm,
            AuthorId? authorId,
            int pageCount,
            int pageSize)
        {
            var query = _dbContext.Posts.AsQueryable();


            if(authorId is not null)
            {
                query = query.Where(p => p.AuthorId == authorId);
            }

            if(!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Title.Contains(searchTerm) || p.Content.Contains(searchTerm));
            }

            int postCount = await query.CountAsync();

            var sortedQuery = sortBy switch
            {
                "newest" => query.OrderByDescending(p => p.CreatedDateTime),
                "oldest" => query.OrderBy(p => p.CreatedDateTime),
                "mostpopular" => query.OrderByDescending(p => p.Likes.Value),
                "leastpopular" => query.OrderBy(p => p.Likes.Value),
                "mostcommented" => query.OrderByDescending(p => p.CommentIds.Count),
                _ => query
            };

            var paginatedQuery = sortedQuery
                .Skip((pageCount - 1) * pageSize)
                .Take(pageSize);

            var posts = await paginatedQuery.ToListAsync();

            return (posts, postCount);
        }

        public async Task UpdateAsync(Post post)
        {
            _dbContext.Update(post);
            await _dbContext.SaveChangesAsync();
        }
    }
}
