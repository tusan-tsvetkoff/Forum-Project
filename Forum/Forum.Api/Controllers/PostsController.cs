using ErrorOr;
using Forum.Application.Posts.Commands.CreatePost;
using Forum.Application.Posts.Commands.DeletePost;
using Forum.Application.Posts.Queries.ListPosts;
using Forum.Contracts.Post;
using Forum.Data.Common.Errors;
using Forum.Infrastructure.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Forum.Api.Controllers;

[Route("api/posts")]
public class PostsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly JwtSettings _jwtSettings;

    public PostsController(IMapper mapper, IMediator mediator, IOptions<JwtSettings> jwtSettings)
    {
        _mapper = mapper;
        _mediator = mediator;
        _jwtSettings = jwtSettings.Value;
    }

/*    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostRequest request, string authorId)
    {
        var command = _mapper.Map<CreatePostCommand>((request, authorId));

        var createPostResult = await _mediator.Send(command);

        return createPostResult.Match(
            post => Ok(_mapper.Map<PostResponse>(post)),
            errors => Problem(errors));
    }*/

    [HttpPost("")]
    public async Task<IActionResult> CreatePost(CreatePostRequest request, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        string token = ExtractTokenFromAuthorizationHeader(authorizationHeader);
        string userId = GetUserIdFromToken(token);

        var command = _mapper.Map<CreatePostCommand>((request, userId));

        var createPostResult = await _mediator.Send(command);

        return createPostResult.Match(
            post => Ok(_mapper.Map<PostResponse>(post)),
            errors => Problem(errors));
    }

    [HttpGet()]
    public async Task<IActionResult> ListPosts(string authorId)
    {
        var query = _mapper.Map<ListPostsQuery>(authorId);

        var listPostsQuery = await _mediator.Send(query);

        return listPostsQuery.Match(
            posts => Ok(posts.Select(post=> _mapper.Map<PostResponse>(post))),
            errors => Problem(errors));
    }
    private static string ExtractTokenFromAuthorizationHeader(string authorizationHeader)
    {
        return authorizationHeader?.Replace("Bearer ", string.Empty);
    }

    private string GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);      
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        //var userId = userIdClaims;
        return userId;
    }
     

    [HttpPost]
    [Route("api/posts/{postId}/like")]
    public IActionResult LikePost(string postId)
    {
        // Find the post by postId and perform the necessary logic to increment the like count
        // Update the post in the data store

        return Ok(); // Return a 200 OK response indicating the like operation was successful
    }

    [HttpPost]
    [Route("api/posts/{postId}/dislike")]
    public IActionResult DislikePost(string postId)
    {
        // Find the post by postId and perform the necessary logic to increment the dislike count
        // Update the post in the data store

        return Ok(); // Return a 200 OK response indicating the dislike operation was successful
    }

    [HttpPost]
    [Route("api/posts/{postId}/comment")]
    public IActionResult CommentPost(string content, string authorId)
    {
        // Find the post by postId and perform the logic to map the comment to the post
        // Update the post and also add the post in the User's post list

        return Ok(); // Return 200 OK response, or any errors with ErrorOr
    }

    [HttpDelete("{postId:guid}")]
    public async Task<IActionResult> DeletePost([FromRoute] Guid postId, [FromHeader(Name = "Authorization")] string authorizationHeader)
    {
        string token = ExtractTokenFromAuthorizationHeader(authorizationHeader);
        string userId = GetUserIdFromToken(token);

        ErrorOr<Guid> userIdResult = Guid.TryParse(userId, out var resultId)
            ? resultId
            : Errors.Authentication.InvalidGuid;

        if(userIdResult.IsError)
        {
            return Problem(userIdResult.ErrorsOrEmptyList);
        }

        var command = _mapper.Map<DeletePostCommand>((postId, resultId));

        var commandResult = await _mediator.Send(command);

        return commandResult.Match(
            deleted => Ok(StatusCodes.Status204NoContent),
            errors => Problem(errors));
    }
}
