using Forum.Application.Comments.Commands;
using Forum.Contracts.Comment;
using Forum.Data.CommentAggregate;
using Mapster;

namespace Forum.Api.Common.Mapping
{
    public class CommentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateCommentRequest Request, Guid UserId, Guid PostId), CreateCommentCommand>()
                .Map(dest => dest, src => src.Request)
                .Map(dest => dest.AuthorId, src => src.UserId)
                .Map(dest => dest.PostId, src => src.PostId);

            config.NewConfig<(Comment, string AuthorUserName), CommentResponse>()
                .Map(dest => dest.Author, src => src.AuthorUserName)
                .Map(dest => dest.Id, src => src.Item1.Id.Value.ToString())
                .Map(dest => dest.PostId, src => src.Item1.PostId.Value.ToString())
                .Map(dest => dest.Content, src => src.Item1.Content)
                .Map(dest => dest.Timestamp, src => src.Item1.CreatedAt.ToString("dd-MM-yy HH:mm:ss"));
        }
    }
}
