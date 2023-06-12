using Forum.Application.Users.Commands.UpdateCountry;
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

        public UsersApiController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            var command = _mapper.Map<UpdateCountryCommand>(request);

            var updateResult = await _mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Errors.User.NotFound)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: updateResult.FirstError.Description);
            }

            return updateResult.Match(
                    updateResult => Ok(_mapper.Map<UpdateUserResponse>(updateResult)),
                    errors => Problem(errors));
        }
    }
}
