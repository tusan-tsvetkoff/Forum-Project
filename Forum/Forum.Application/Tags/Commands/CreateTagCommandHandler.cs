using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.Common.Errors;
using Forum.Data.TagEntity;
using MediatR;

namespace Forum.Application.Tags.Commands;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ErrorOr<Tag>>
{
    private readonly ITagRepository _tagRepository;

    public CreateTagCommandHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<ErrorOr<Tag>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        if( await _tagRepository.Exists(request.Name) is true)
        {
            return Errors.Tag.TagAlreadyExists(request.Name);
        }
        var tag = Tag.Create(request.Name);
        await _tagRepository.AddAsync(tag);
        return tag;
    }
}
