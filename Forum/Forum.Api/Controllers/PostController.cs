using Forum.Application.Services.Post;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostController:ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService _postService)
        {
            this._postService = _postService;
        }


    }
}
