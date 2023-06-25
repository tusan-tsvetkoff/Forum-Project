using ErrorOr;
using Forum.Api.Common.Helpers;
using Forum.Application.Posts.Commands.CreatePost;
using Forum.Application.Posts.Commands.DeletePost;
using Forum.Application.Posts.Queries.GetPost;
using Forum.Application.Posts.Queries.ListPosts;
using Forum.Contracts.Post;
using Forum.Data.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Quic;
using System.Security.Claims;

namespace Forum.Api.Controllers;

[Route("api/posts")]
public class PostsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public PostsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreatePost(CreatePostRequest request)
    {
        var userIdentity = User.Identity as ClaimsIdentity;
        var authId = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        ErrorOr<Guid> userIdResult = Guid.TryParse(authId, out var userId)
                ? userId
                : Errors.Authentication.InvalidGuid;

        if (userIdResult.IsError && userIdResult.FirstError == Errors.Authentication.InvalidGuid)
        {
            return Problem(
                statusCode: StatusCodes.Status415UnsupportedMediaType,
                title: userIdResult.FirstError.Description);
        }

        var command = _mapper.Map<CreatePostCommand>((request, userId));

        var createPostResult = await _mediator.Send(command);

        return createPostResult.Match(
            post => Ok(_mapper.Map<PostResponse>(post)),
            errors => Problem(errors));
    }

    [HttpGet("{postId:guid}")]
    public async Task<IActionResult> GetPost([FromRoute] Guid postId)
    {
        var query = _mapper.Map<GetPostQuery>(postId);

        var getPostQuery = await _mediator.Send(query);

        return getPostQuery.Match(
            post => Ok(_mapper.Map<PostResponse>(post)),
            errors => Problem(errors));
    }

    [HttpGet()]
    public async Task<IActionResult> GetPosts(
        [FromQuery] GetPostsQueryParams queryParams)
    {
        var request = _mapper.Map<GetPostsQuery>(queryParams);
        var listPostsResult = await _mediator.Send(request);

        return listPostsResult.Match(
               posts =>
               {
                   var postResponseList = new PostResponseListNew(Posts: posts.Item1, PageInfo: posts.Item2);
                   return Ok(postResponseList);
               },
               errors => Problem());
    }

    [HttpDelete("{postId:guid}")]
    public async Task<IActionResult> DeletePost(
        [FromRoute] Guid postId)
    {
        var userIdentity = User.Identity as ClaimsIdentity;
        var authId = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        ErrorOr<Guid> userIdResult = Guid.TryParse(authId, out var userId)
                ? userId
                : Errors.Authentication.InvalidGuid;
        if (userIdResult.IsError)
        {
            return Problem(
                statusCode: StatusCodes.Status415UnsupportedMediaType,
                title: userIdResult.FirstError.Description);
        }

        var command = _mapper.Map<DeletePostCommand>((postId, userId));

        var commandResult = await _mediator.Send(command);

        return commandResult.Match(
            deleted => Ok(StatusCodes.Status204NoContent),
            errors => Problem(errors));
    }
}
