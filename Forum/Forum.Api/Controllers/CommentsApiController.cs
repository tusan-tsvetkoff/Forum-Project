﻿using ErrorOr;
using Forum.Api.Common.Helpers;
using Forum.Contracts.Comment;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Forum.Data.Common.Errors;
using Forum.Application.Comments.Commands;
using Mapster;

namespace Forum.Api.Controllers;

[Route("api/comments")]
public class CommentsApiController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserIdProvider _userIdProvider;

    public CommentsApiController(IMapper mapper, IMediator mediator, IUserIdProvider userIdProvider)
    {
        _mapper = mapper;
        _mediator = mediator;
        _userIdProvider = userIdProvider;
    }

    [HttpPost("{postId:guid}")]
    public async Task<IActionResult> CreateCommentAsync(
        [FromRoute] Guid postId,
        [FromBody] CreateCommentRequest request,
        [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        var token = ExtractTokenFromAuthorizationHeader(authorizationHeader);
        var userId = _userIdProvider.GetUserId(token);

        ErrorOr<Guid> userIdResult = Guid.TryParse(userId, out var resultUserId)
            ? resultUserId
            : Errors.Authentication.InvalidGuid;

        if (userIdResult.IsError)
        {
            return Problem(userIdResult.Errors);
        }
        
        var command = _mapper.Map<CreateCommentCommand>((request, resultUserId, postId));

        var commandResult = await _mediator.Send(command);

        return commandResult.Match(
            comment => Ok(_mapper.Map<CommentResponse>(comment)),
            errors => Problem(errors));
    }

    private static string ExtractTokenFromAuthorizationHeader(string authorizationHeader)
    {
        return authorizationHeader?.Replace("Bearer ", string.Empty);
    }

}