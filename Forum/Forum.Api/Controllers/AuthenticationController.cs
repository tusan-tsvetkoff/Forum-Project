using Forum.Application.Authentication.Commands.Register;
using Forum.Application.Authentication.Queries.Login;
using Forum.Contracts.Authentication;
using Forum.Data.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers;


[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly LinkGenerator _linkGenerator;

    public AuthenticationController(ISender mediator, IMapper mapper, LinkGenerator linkGenerator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _linkGenerator = linkGenerator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        var authResult = await _mediator.Send(command);

/*        var location = _linkGenerator.GetPathByAddress<RouteValuesAddress>(
            HttpContext,
            values =>
            {
                values.Controller = "Authentication";
                values.UserId = authResult.Value.User.Id.Value;
            });*/
            

        return authResult.Match(
            authResult => Created(nameof(Register), _mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }
}
