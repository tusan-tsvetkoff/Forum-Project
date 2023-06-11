using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Persistence
{
    public class PostRepository : IPostRepository
    {
        private static readonly List<Post> _posts = new List<Post>();

        public List<Post> GetPosts()
        {
           return _posts.ToList();
        }
    }
}
