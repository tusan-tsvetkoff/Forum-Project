using Forum.Application.Comments.Commands;
using Forum.Application.Comments.Commands.Delete;
using Forum.Application.Comments.Common;
using Forum.Application.Comments.Queries.Get;
using Forum.Application.Comments.Queries.GetList;
using Forum.Contracts.Comment;
using Forum.Data.UserAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Api.Controllers;

[Route("api/posts")]
public class CommentsApiController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CommentsApiController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("{postId:guid}/comments")]
    public async Task<IActionResult> CreateCommentAsync(
        [FromRoute] Guid postId,
        [FromBody] CreateCommentRequest request)
    {
        var userIdentity = User.Identity as ClaimsIdentity;
        var authId = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var userId = Guid.Parse(authId!);

        var command = _mapper.Map<CreateCommentCommand>((request, userId, postId));

        var commandResult = await _mediator.Send(command);

        return commandResult.Match(comment =>
        {
            var response = _mapper.Map<CommentResponse>((commandResult.Value.Item1, commandResult.Value.AuthorUsername));
            return Created(nameof(CreateCommentAsync), response);
        },
        error => Problem(error));
    }

    [HttpGet("{postId:guid}/comments/{id:guid}")]
    public async Task<IActionResult> GetCommentAsync(
        [FromRoute] GetCommentRequest request)
    {
        var query = _mapper.Map<GetCommentQuery>(request);

        var queryResult = await _mediator.Send(query);

        return queryResult.Match(
            comment =>
            {
                var response = _mapper.Map<CommentResponse>(comment);
                return Ok(response);
            },
            error => Problem(error));
    }

    [HttpGet("{postId:guid}/comments")]
    public async Task<IActionResult> GetCommentsAsync(
        [FromRoute] GetCommentsQueryParams queryParams,
        [FromRoute] Guid postId)
    {
        var query = _mapper.Map<GetCommentsQuery>((postId, queryParams));

        var queryResult = await _mediator.Send(query);
        

        return queryResult.Match(
            comments =>
            {
                var commentResponseList = new ListCommentResponse(PostId: postId.ToString(), comments.Item1, comments.Item2);
                return Ok(commentResponseList);
            },
            error => Problem(error));
    }

    [HttpDelete("{postId:guid}/comments/{id:guid}")]
    public async Task<IActionResult> DeleteCommentAsync(
        [FromRoute] DeleteCommentRequest request)
    {
        GetUserId(out Guid userId);

        var command = _mapper.Map<DeleteCommentCommand>((request, userId));

        var commandResult = await _mediator.Send(command);

        return commandResult.Match(
            deleted => NoContent(),
            error => Problem(error));
    }

    private void GetUserId(out Guid userId)
    {
        var userIdentity = User.Identity as ClaimsIdentity;
        var authId = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        userId = Guid.Parse(authId!);
    }
}
