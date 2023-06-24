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
            var request = new GetMostCommentedPublicRequest();
            var query = _mapper.Map<ListPostsQuery>(request);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                posts => Ok(_mapper.Map<List<PostResponse>>(posts)),
                errors => Problem());
        }

        [HttpGet("posts/most-recent")]
        public async Task<IActionResult> GetMostRecentPosts()
        {
            var request = new GetMostRecentPublicRequest();

            var query = _mapper.Map<ListPostsQuery>(request);

            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
                posts => Ok(_mapper.Map<List<PostResponse>>(posts)),
                errors => Problem());
        }
    }
}
