using Forum.Application.Posts.Commands.CreatePost;
using Forum.Application.Posts.Commands.DeletePost;
using Forum.Application.Posts.Queries.GetPost;
using Forum.Application.Posts.Queries.ListPosts;
using Forum.Contracts.Post;
using Forum.Data.PostAggregate;
using Mapster;

namespace Forum.Api.Common.Mapping;


public class PostMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreatePostRequest Request, Guid UserId), CreatePostCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest, src => src.Request);

        // Ceate mapping for the list posts query
        config.NewConfig<ListPostsRequest, ListPostsQuery>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.Search, src => src.Search)
            .Map(dest => dest.Sort, src => src.Sort)
            .Map(dest => dest.Username, src => src.Username);

        // Create mapping for query params to list posts request
        config.NewConfig<(int Page, int PageSize, string Search, string Sort, string Username), ListPostsRequest>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.Search, src => src.Search)
            .Map(dest => dest.Sort, src => src.Sort)
            .Map(dest => dest.Username, src => src.Username);

        config.NewConfig<Guid, GetPostQuery>()
            .Map(dest => dest.PostId, src => src);

        config.NewConfig<(Guid PostId, Guid UserId), DeletePostCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.PostId, src => src.PostId);

        config.NewConfig<Post, PostResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.AuthorId, src => src.AuthorId.Value.ToString());
    }
}

