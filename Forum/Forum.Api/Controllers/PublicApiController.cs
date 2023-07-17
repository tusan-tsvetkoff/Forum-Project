using Forum.Application.Posts.Queries.ListPosts;
using Forum.Application.Public.Queries.GetTags;
using Forum.Application.Public.Statistics.Queries;
using Forum.Contracts.Common;
using Forum.Contracts.Post;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers;

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

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var query = new GetStatisticsQuery(
            true);

        var queryResult = await _mediator.Send(query);

        return queryResult.Match(
                statistics => Ok(statistics),
                errors => Problem());
    }

    [HttpGet("tags")]
    public async Task<IActionResult> GetTags()
    {
        var query = new GetTagsQuery();

        var queryResult = await _mediator.Send(query);

        return queryResult.Match(
                       tags => Ok(tags.Select(t => _mapper.Map<TagsResponse>(t))),
                       errors => Problem());
    }
}
