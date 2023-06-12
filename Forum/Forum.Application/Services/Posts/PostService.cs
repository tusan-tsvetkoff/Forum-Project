using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.Entities;
using Forum.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services.Posts
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        /*        public List<Post> GetPosts()
                {
                    var posts = _postRepository.GetPosts();

                    foreach (var post in posts)
                    {
                        PostResult result = new PostResult(
                            Id: post.Id,
                            Title: post.Title,
                            Content: post.Content,
                            UserId: post.UserId,
                          // Comments:post.comment,
                          CreatedDate: post.CreatedDate,
                          LastEditedDate: post.LastEditedDate);
                    }

                }*/
    }
}
