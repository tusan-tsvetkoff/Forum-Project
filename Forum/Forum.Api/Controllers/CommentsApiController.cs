using Forum.Application.Comments.Commands;
using Forum.Contracts.Comment;
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
}
