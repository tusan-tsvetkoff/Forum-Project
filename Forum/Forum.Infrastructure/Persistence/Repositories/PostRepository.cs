using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private static readonly List<Post> _posts = new();


        // the await Task.CompletedTask are placeholders until I implement the DB (just so it stops being annoying about it).
        public async Task AddAsync(Post post)
        {
            await Task.CompletedTask;
            _posts.Add(post);
        }

        public async Task<Post> GetByIdAsync(PostId postId)
        {
            return _posts.SingleOrDefault(post => post.Id == postId);
        }

        public async Task<List<Post>> ListAsync(AuthorId authorId)
        {
            await Task.CompletedTask;
            return _posts.Where(post => post.UserId == authorId).ToList();
        }

        public async Task DeleteAsync(PostId postId)
        {
            var postToRemove = _posts.SingleOrDefault(post => post.Id == postId);
            if (postToRemove != null)
            {
                _posts.Remove(postToRemove);
            }
            await Task.CompletedTask;
        }

        public async Task<int> GetPostCountAsync()
        {
            await Task.CompletedTask;
            return _posts.Count;
        }

        public async Task<List<Post>> GetMostRecentAsync()
        {
            await Task.CompletedTask;
            return _posts
                    .OrderByDescending(post => post.CreatedDateTime)
                    .Take(10)
                    .ToList();
        }

        public async Task<List<Post>> GetMostCommentedAsync()
        {
            await Task.CompletedTask;
            return _posts
                .OrderByDescending(post => post.Comments.Count)
                .Take(10)
                .ToList();
        }
    }
}
