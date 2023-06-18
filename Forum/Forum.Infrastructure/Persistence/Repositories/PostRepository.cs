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
        private static List<Post> _posts = new List<Post>();
        private readonly ForumDbContext _dbContext;
        public PostRepository(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // the await Task.CompletedTask are placeholders until I implement the DB (just so it stops being annoying about it).
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

        public async Task DeleteAsync(PostId postId)
        {
            
        }

        public Task<int> GetPostCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetPostsAsync(string sort, string? authorId, int page, int pageSize, string search)
        {
            await Task.CompletedTask;
            var postQuery = _posts.AsQueryable();



            if (!string.IsNullOrEmpty(authorId))
            {
                postQuery = postQuery.Where(p => p.AuthorId.Value == AuthorId.Create(authorId).Value);
            }

            if (!string.IsNullOrEmpty(search))
            {
                postQuery = postQuery.Where(p => p.Title.Contains(search) || p.Content.Contains(search));
            }

            // TODO: Add support for multiple sort parameters at once
            switch (sort)
            {
                case "newest":
                    postQuery = postQuery.OrderByDescending(p => p.CreatedDateTime);
                    break;
                case "oldest":
                    postQuery = postQuery.OrderBy(p => p.CreatedDateTime);
                    break;
                case "mostpopular":
                    postQuery = postQuery.OrderByDescending(p => p.Likes.Value);
                    break;
                case "leastpopular":
                    postQuery = postQuery.OrderBy(p => p.Likes.Value);
                    break;
                case "mostcommented":
                    postQuery = postQuery.OrderByDescending(p => p.CommentIds.Count);
                    break;
            }

            int skipCount = (page - 1) * pageSize;

            postQuery = postQuery.Skip(skipCount)
                                 .Take(pageSize);
            return postQuery.ToList();
        }
    }
}
