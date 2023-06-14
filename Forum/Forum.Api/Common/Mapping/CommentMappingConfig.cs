using Forum.Application.Comments.Commands;
using Forum.Contracts.Comment;
using Forum.Data.PostAggregate.Entities;
using Mapster;

namespace Forum.Api.Common.Mapping
{
    public class CommentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateCommentRequest Request, Guid UserId, Guid PostId), CreateCommentCommand>()
                .Map(dest => dest, src => src.Request)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.PostId, src => src.PostId);

            config.NewConfig<Comment, CommentResponse>()
                .Map(dest => dest.CommentId, src => src.Id.Value.ToString())
                .Map(dest => dest.UserId, src => src.UserId.Value.ToString())
                .Map(dest => dest.Content, src => src.Content)
                .Map(dest => dest.CreatedDateTime, src => src.CreatedDateTime);
        }
    }
}
