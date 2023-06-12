using Forum.Application.Comments.CreateComment;
using Forum.Contracts.Comments;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers
{
    [Route("/posts/{postId}/comments")]
    public class CommentsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public CommentsController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(
            CreateCommentRequest request,
            [FromHeader(Name = "Authorization")] string authorizationHeader,
            string postId)
        {
            var command = _mapper.Map<CreateCommentCommand>((request, postId));

            var createCommentResult = await _mediator.Send(command);
            return Ok(createCommentResult);
        }
    }
}
