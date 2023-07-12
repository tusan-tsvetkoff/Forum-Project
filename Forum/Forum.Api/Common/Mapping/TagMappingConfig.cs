using Forum.Application.Tags.Commands;
using Forum.Application.Tags.Queries;
using Forum.Contracts.Common;
using Forum.Contracts.Tags;
using Forum.Data.TagEntity;
using Mapster;

namespace Forum.Api.Common.Mapping;

public class TagMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Tag, TagsResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<CreateTagRequest, CreateTagCommand>()
            .Map(dest => dest.Name, src => src.Name);

        config.NewConfig<Guid, GetTagQuery>()
            .Map(dest => dest.Id, src => src);
    }
}
