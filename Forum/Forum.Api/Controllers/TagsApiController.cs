using Forum.Application.Tags.Commands;
using Forum.Application.Tags.Queries;
using Forum.Contracts.Common;
using Forum.Contracts.Tags;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers;

[Route("api/tags")]
public class TagsApiController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TagsApiController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag(
               [FromBody] CreateTagRequest request)
    {
        var command = _mapper.Map<CreateTagCommand>(request);

        var result = await _mediator.Send(command);

        return result.Match(
                       created => CreatedAtAction(nameof(GetTag), new { tagId = created.Id }, created),
                       errors => Problem());
    }

    [HttpGet("{tagId:guid}")]
    public async Task<IActionResult> GetTag(
               [FromRoute] Guid tagId)
    {
        var query = _mapper.Map<GetTagQuery>(tagId);

        var result = await _mediator.Send(query);

        return result.Match(
                       tag => Ok(_mapper.Map<TagsResponse>(tag)),
                       errors => Problem());
    }
}
