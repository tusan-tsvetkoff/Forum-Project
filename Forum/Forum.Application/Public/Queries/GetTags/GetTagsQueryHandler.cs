using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.TagEntity;
using MediatR;

namespace Forum.Application.Public.Queries.GetTags;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, ErrorOr<List<Tag>>>
{
    private readonly ITagRepository _tagRepository;

    public GetTagsQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ErrorOr<List<Tag>>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _tagRepository.GetTagsAsync();
    }
}
