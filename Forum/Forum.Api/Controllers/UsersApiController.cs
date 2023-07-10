using ErrorOr;
using Forum.Application.Users.Commands.Delete;
using Forum.Application.Users.Commands.Update;
using Forum.Application.Users.Queries;
using Forum.Application.Users.Queries.GetUser;
using Forum.Contracts.Post;
using Forum.Contracts.User;
using Forum.Data.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Api.Controllers;

[Route("api/users")]
public class UsersApiController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UsersApiController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(
        [FromRoute] Guid userId)
    {
        GetUserId(out Guid authUserId);

        if (userId != authUserId)
        {
            return Unauthorized();
        }

        var command = _mapper.Map<DeleteUserCommand>(userId);

        var result = await _mediator.Send(command);

        return result.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }

    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> UpdateUser(
        [FromBody] UpdateProfileRequest request,
        [FromRoute] Guid userId)
    {
        GetUserId(out Guid authUserId);

        if (userId != authUserId)
        {
            return Unauthorized();
        }
        var command = _mapper.Map<UpdateProfileCommand>((request, userId));

        return Ok();
    }


    /// <summary>
    /// This method is an HTTP GET endpoint that retrieves a paginated list of users based on the provided query parameters.
    /// </summary>
    /// <param name="queryParams">The parameters by which to sort and filter the results.</param>
    /// <returns>A list of users, with pagination, based on the query parameters.</returns>
    [HttpGet()]
    public async Task<IActionResult> GetUsers(
        [FromQuery] GetUserQueryParams queryParams)
    {
        var searchQuery = _mapper.Map<GetUsersQuery>(queryParams);

        var result = await _mediator.Send(searchQuery);

        return result.Match(
               users =>
               {
                   var userResponseList = users.Item1.Select(u => _mapper.Map<UserResponse>(u)).ToList();
                   var resultTuple = new UserResponseList(Users: userResponseList, PageInfo: users.Item2);
                   return Ok(resultTuple);
               },
               errors => Problem());
    }

    [HttpGet("{authorId}")]
    public async Task<IActionResult> GetAuthor(string authorId)
    {

        var request = new GetAuthorRequest(authorId);
        var query = _mapper.Map<GetAuthorQuery>(request);

        var result = await _mediator.Send(query);

        return result.Match(
                       author => Ok(_mapper.Map<AuthorResponse>(author)),
                                  errors => Problem());
    }

    private void GetUserId(out Guid userId)
    {
        var userIdentity = User.Identity as ClaimsIdentity;
        var authId = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        userId = Guid.Parse(authId!);
    }
}
