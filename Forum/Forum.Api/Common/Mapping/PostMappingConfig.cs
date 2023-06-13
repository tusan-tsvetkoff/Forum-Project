using Forum.Application.Posts.Commands.CreatePost;
using Forum.Application.Posts.Commands.DeletePost;
using Forum.Application.Posts.Queries.ListPosts;
using Forum.Contracts.Post;
using Forum.Data.PostAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using Mapster;

namespace Forum.Api.Common.Mapping;


public class PostMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreatePostRequest Request, string userId), CreatePostCommand>()
            .Map(dest => dest.UserId, srsc => srsc.userId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<string, ListPostsQuery>()
            .MapWith(src => new ListPostsQuery(src));

        config.NewConfig<(Guid PostId, Guid UserId), DeletePostCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.PostId, src => src.PostId);

        config.NewConfig<Post, PostResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.UserId, src => src.UserId.Value);
    }
}
