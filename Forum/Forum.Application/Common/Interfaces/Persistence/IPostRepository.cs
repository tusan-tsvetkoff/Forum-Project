using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Common.Interfaces.Persistence
{
    public interface IPostRepository
    {
        Task AddAsync(Post post);
        Task DeleteAsync(PostId postId);
        Task<Post> GetByIdAsync(PostId postId);
        Task<int> GetPostCountAsync();
        Task<List<Post>> ListAsync(AuthorId authorId);
    }
}
