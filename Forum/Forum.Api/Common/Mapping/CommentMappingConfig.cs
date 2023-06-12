using Forum.Application.Comments.CreateComment;
using Forum.Contracts.Comments;
using Forum.Data.Commenter.ValueObjects;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Common.Mapping
{
    public class CommentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateCommentRequest Request, string PostId), CreateCommentCommand>()
                .Map(dest => dest.PostId, src => src.PostId)
                .Map(dest => dest, src => src.Request);
        }
    }
}
