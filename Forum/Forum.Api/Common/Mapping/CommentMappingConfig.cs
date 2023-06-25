using Forum.Application.Comments.Commands;
using Forum.Application.Comments.Commands.Delete;
using Forum.Application.Comments.Common;
using Forum.Application.Comments.Queries.Get;
using Forum.Application.Comments.Queries.GetList;
using Forum.Contracts.Comment;
using Forum.Contracts.Post;
using Forum.Data.CommentAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Mapster;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            config.NewConfig<CommentResult, CommentResponse>()
                .Map(dest => dest.Author, src => src.Author);

            config.NewConfig<GetCommentRequest, GetCommentQuery>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.PostId, src => src.PostId);

            config.NewConfig<(DeleteCommentRequest, Guid UserId), DeleteCommentCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.Id, src => src.Item1.Id)
                .Map(dest => dest.PostId, src => src.Item1.PostId);

            config.NewConfig<Comment, PostCommentResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Author, src => src.AuthorId.Value)
                .Map(dest => dest.Content, src => src.Content)
                .Map(dest => dest.Timestamp, src => src.CreatedAt.ToString("dd-MM-yy HH:mm:ss"));

            config.NewConfig<(Guid, GetCommentsQueryParams), GetCommentsQuery>()
                .Map(dest => dest.PostId, src => src.Item1)
                .Map(dest => dest, src => src.Item2);
                
        }   
    }
}
