using ErrorOr;
using Forum.Api.Common.Helpers;
using Forum.Application.Users.Commands.UpdateProfile;
using Forum.Contracts.User;
using Forum.Data.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers
{
    [Route("users")]
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

        [HttpPut("profile")]
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

        private static string ExtractTokenFromAuthorizationHeader(string authorizationHeader)
        {
            return authorizationHeader?.Replace("Bearer ", string.Empty)!;
        }
    }
}
