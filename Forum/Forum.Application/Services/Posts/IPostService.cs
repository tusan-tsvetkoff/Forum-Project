using Forum.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services.Posts
{
    public interface IPostService
    {
        //public PostResponse GetPost(Guid PostId);
        //public PostResponse CreatePost(Guid UserId, PostRequest postRerquest);
        //public PostResponse UpdatePost(Guid postId, PostRequest postRequest);
        //public bool DeletePost(Guid postId);
        public List<Post> GetPosts();
    }
}
