using Microsoft.AspNetCore.Mvc;
using Forum.Application.Services.Authentication;
using Forum.Contracts.Authentication;

namespace Forum.Api.Controllers;

    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Username,
                request.Email,
                request.Password);

                var response = new AuthenticationResult(
                    authResult.Id,
                    authResult.FirstName,
                    authResult.LastName,
                    authResult.Username,
                    authResult.Email,
                    authResult.Token
                );
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(
                request.Email,
                request.Password);
                var response = new AuthenticationResult(
                    authResult.Id,
                    authResult.FirstName,
                    authResult.LastName,
                    authResult.Username,
                    authResult.Email,
                    authResult.Token
                );   
            return Ok(response);
        }
    }
