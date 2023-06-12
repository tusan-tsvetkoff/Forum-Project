using Forum.Application.Posts.Commands.CreatePost;
using Forum.Application.Posts.Queries.ListPosts;
using Forum.Application.Services.Posts;
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
}
