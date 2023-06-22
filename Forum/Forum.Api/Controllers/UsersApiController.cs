using ErrorOr;
using Forum.Api.Common.Helpers;
using Forum.Application.Users.Commands.Delete;
using Forum.Application.Users.Commands.UpdateProfile;
using Forum.Application.Users.Queries;
using Forum.Contracts.User;
using Forum.Data.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Api.Controllers
{
    [Route("api/users")]
    public class UsersApiController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserIdProvider _userIdProvider;

        public UsersApiController(IMapper mapper, IMediator mediator, IUserIdProvider userIdProvider)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userIdProvider = userIdProvider;
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> UpdateProfile(
            [FromHeader(Name = "Authorization")] string authorizationHeader,
            [FromBody] UpdateProfileRequest request)
        {
            string token = ExtractTokenFromAuthorizationHeader(authorizationHeader);
            string idFromToken = _userIdProvider.GetUserId(token);

            ErrorOr<Guid> userIdResult = Guid.TryParse(idFromToken, out var userId)
                    ? userId
                    : Errors.Authentication.InvalidGuid;

            if (userIdResult.IsError && userIdResult.FirstError == Errors.Authentication.InvalidGuid)
            {
                return Problem(
                    statusCode: StatusCodes.Status415UnsupportedMediaType,
                    title: userIdResult.FirstError.Description);
            }

            var command = _mapper.Map<UpdateProfileCommand>((request, userId));

            var result = await _mediator.Send(command);

            return result.Match(
                updated => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUser(
            [FromRoute] Guid userId)
        {
            var userIdentity = User.Identity as ClaimsIdentity;
            var authId = userIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ErrorOr<Guid> userIdResult = Guid.TryParse(authId, out var guidResult)
                    ? guidResult
                    : Errors.Authentication.InvalidGuid;

            if (userIdResult.IsError && userIdResult.FirstError == Errors.Authentication.InvalidGuid)
            {
                return Problem(
                statusCode: StatusCodes.Status415UnsupportedMediaType,
                title: userIdResult.FirstError.Description);
            }

            if (userId != guidResult)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: "Unauthorized."); // TODO: Add error message, this is just a placeholder.
            }
            // A lot of boilerplate above, but it's all necessary to get the user id from the token.

            var command = _mapper.Map<DeleteUserCommand>(guidResult);

            var result = await _mediator.Send(command);

            return result.Match(
                deleted => NoContent(),
                errors => Problem(errors));
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
                       var resultTuple = new UserResponseList(Users: userResponseList,PageInfo: users.Item2);
                       return Ok(resultTuple);
                   },
                   errors => Problem());
        }

        private static string ExtractTokenFromAuthorizationHeader(string authorizationHeader)
        {
            return authorizationHeader?.Replace("Bearer ", string.Empty)!;
        }
    }
}
