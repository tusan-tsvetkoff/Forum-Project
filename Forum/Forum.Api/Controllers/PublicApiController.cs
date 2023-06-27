using Forum.Application.Posts.Queries.ListPosts;
using Forum.Contracts.Post;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers
{
    [Route("api/public")]
    [AllowAnonymous]
    public class PublicApiController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PublicApiController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("posts/most-commented")]
        public async Task<IActionResult> GetMostCommentedPosts()
        {
            var request = new GetPostsQueryParams
            (
                null,
                "comments",
                "desc",
                null,
                1,
                10
            );

            var query = _mapper.Map<GetPostsQuery>(request);
            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                posts =>
                {
                    var postResponseList = new PostResponseListNew(Posts: posts.Item1, PageInfo: posts.Item2);
                    return Ok(postResponseList);
                },
                errors => Problem());
        }

        [HttpGet("posts/most-recent")]
        public async Task<IActionResult> GetMostRecentPosts()
        {
            var request = new GetPostsQueryParams
            (
                null,
                "created",
                "desc",
                null,
                1,
                10);

            var query = _mapper.Map<GetPostsQuery>(request);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                posts =>
                {
                    var postResponseList = new PostResponseListNew(Posts: posts.Item1, PageInfo: posts.Item2);
                    return Ok(postResponseList);
                },
                errors => Problem());
        }
    }
}
