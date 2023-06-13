using Forum.Application.Posts.Commands.CreatePost;
using Forum.Application.Posts.Queries.ListPosts;
using Forum.Contracts.Post;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers;

[Route("posts/{authorId}")]
public class PostsController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public PostsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostRequest request, string authorId)
    {
        var command = _mapper.Map<CreatePostCommand>((request, authorId));

        var createPostResult = await _mediator.Send(command);

        return createPostResult.Match(
            post => Ok(_mapper.Map<PostResponse>(post)),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> ListPosts(string authorId)
    {
        var query = _mapper.Map<ListPostsQuery>(authorId);

        var listPostsQuery = await _mediator.Send(query);

        return listPostsQuery.Match(
            posts => Ok(posts.Select(post=> _mapper.Map<PostResponse>(post))),
            errors => Problem(errors));
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
}
