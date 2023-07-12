using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.Common.Errors;
using Forum.Data.TagEntity;
using Forum.Data.TagEntity.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Tags.Queries;

public class GetTagQueryHandler : IRequestHandler<GetTagQuery, ErrorOr<Tag>>
{
    private readonly ITagRepository _tagRepository;

    public GetTagQueryHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ErrorOr<Tag>> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tagId = TagId.Create(request.Id);
        if(await _tagRepository.GetTagByIdAsync(tagId) is not Tag tag)
        {
            return Errors.Tag.TagNotFound(tagId.Value);
        }

        return tag;
    }
}
