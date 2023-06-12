using Forum.Application.Posts.Commands.CreatePost;
using Forum.Application.Posts.Queries.ListPosts;
using Forum.Contracts.Post;
using Mapster;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.AuthorAggregate;
using Forum.Data.PostAggregate;

namespace Forum.Api.Common.Mapping;


public class PostMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreatePostRequest Request, string AuthorId), CreatePostCommand>()
            .Map(dest => dest.AuthorId, srsc => srsc.AuthorId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<string, ListPostsQuery>()
            .MapWith(src => new ListPostsQuery(src));

        config.NewConfig<Post, PostResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.AuthorId, src => src.AuthorId.Value.ToString());
    }
}
