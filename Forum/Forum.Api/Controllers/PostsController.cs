using AspNetCore.Hateoas.Models;
using Forum.Application.Posts.Commands.CreatePost;
using Forum.Application.Posts.Commands.DeletePost;
using Forum.Application.Posts.Commands.UpdatePost;
using Forum.Application.Posts.Queries.GetPost;
using Forum.Application.Posts.Queries.ListPosts;
using Forum.Contracts.Post;
using Forum.Data.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Claims;
using System.Web.Http.Controllers;

namespace Forum.Api.Controllers;

[Route("api/posts")]
public class PostsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUrlHelperFactory _urlHelperFactory;
    public PostsController(IMapper mapper, IMediator mediator, IUrlHelperFactory urlHelperFactory)
    {
        _mapper = mapper;
        _mediator = mediator;
        _urlHelperFactory = urlHelperFactory;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreatePost(CreatePostRequest request)
    {
        GetUserId(out Guid userId);

        var command = _mapper.Map<CreatePostCommand>((request, userId));

        var commandResult = await _mediator.Send(command);

        return commandResult.Match(
            post => Ok(_mapper.Map<PostResponse>(post)),
            errors => Problem());
    }

    [HttpPatch("{postId:guid}")]
    public async Task<IActionResult> UpdatePost(
        [FromRoute] Guid postId,
        [FromBody] UpdatePostRequest request)
    {
        GetUserId(out Guid userId);

        var requestToMap = request with { Id = postId, UserId = userId };
        var command = _mapper.Map<UpdatePostCommand>(requestToMap);
        var commandResult = await _mediator.Send(command);

        if (commandResult.IsError && commandResult.FirstError == Errors.Authentication.UnauthorizedAction)
        {
            return Unauthorized();
        }

        return commandResult.Match(
            updated => NoContent(),
            errors => Problem(errors));
    }

    [HttpGet("{postId:guid}", Name ="GetPost")]
    public async Task<IActionResult> GetPost([FromRoute] Guid postId)
    {
        var query = _mapper.Map<GetPostQuery>(postId);

        var queryResult = await _mediator.Send(query);

        

        return queryResult.Match(
            post => Ok(post),
            errors => Problem());
    }

    [HttpGet()]
    public async Task<IActionResult> GetPosts(
        [FromQuery] GetPostsQueryParams queryParams)
    {
        var query = _mapper.Map<GetPostsQuery>(queryParams);
        var queryResult = await _mediator.Send(query);

        return queryResult.Match(
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
        GetUserId(out Guid userId);

        var command = _mapper.Map<DeletePostCommand>((postId, userId));
        var commandResult = await _mediator.Send(command);

        if (commandResult.IsError && commandResult.FirstError == Errors.Authentication.UnauthorizedAction)
        {
            return Unauthorized();
        }

        return commandResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }

    private void GetUserId(out Guid userId)
    {
        var userIdentity = User.Identity as ClaimsIdentity;
        var authId = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        userId = Guid.Parse(authId!);
    }
}
