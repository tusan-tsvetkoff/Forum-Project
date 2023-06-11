using Forum.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Common.Interfaces.Persistence
{
    public interface IPostRepository
    {
        //public Post GetPost(string title);
        //public Post CreatePost(Guid UserId, Post post);
        //public Post UpdatePost(Guid postId, Post post);
        //public bool DeletePost(Guid postId);
        public List<Post> GetPosts();
    }
}
