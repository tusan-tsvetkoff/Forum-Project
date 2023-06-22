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

        // Create mapping for the get posts query (most commented)
        config.NewConfig<GetMostCommentedPublicRequest, ListPostsQuery>()
            .Map(dest => dest.Sort, src => src.MostCommented)
            .Map(dest => dest.Page, src => src.PageCount)
            .Map(dest =>dest.PageSize, src => src.PageSize);

        config.NewConfig<GetMostRecentPublicRequest, ListPostsQuery>()
            .Map(dest => dest.Sort, src => src.MostRecent)
            .Map(dest => dest.Page, src => src.PageCount)
            .Map(dest => dest.PageSize, src => src.PageSize);

        // Create mapping for query params to list posts request
        config.NewConfig<GetPostsQueryParams, GetPostsQuery>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.SearchTerm, src => src.SearchTerm)
            .Map(dest => dest.SortColumn, src => src.SortColumn)
            .Map(dest => dest.SortOrder, src => src.SortOrder)
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

