using AspNetCore.Hateoas.Models;
using Forum.Application.Posts.Commands.CreatePost;
using Forum.Application.Posts.Commands.DeletePost;
using Forum.Application.Posts.Commands.UpdatePost;
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
            .Map(dest => dest, src => src.Request)
            .Map(dest => dest.Tags, src => src.Request.Tags.Select(tag => tag.ToLowerInvariant()));

        // Create mapping for query params to list posts request
        config.NewConfig<GetPostsQueryParams, GetPostsQuery>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.SearchTerm, src => src.SearchTerm)
            .Map(dest => dest.SortColumn, src => src.SortColumn)
            .Map(dest => dest.SortOrder, src => src.SortOrder)
            .Map(dest => dest.Username, src => src.Username);

        config.NewConfig<Post, (LikesResponse, DislikesResponse)>()
            .Map(dest => dest.Item1, src => src.Likes.Value)
            .Map(dest => dest.Item2, src => src.Dislikes.Value);

        config.NewConfig<Guid, GetPostQuery>()
            .Map(dest => dest.PostId, src => src);

        config.NewConfig<(Guid PostId, Guid UserId), DeletePostCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.PostId, src => src.PostId);

        config.NewConfig<Post, PostResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.Tags, src => src.Tags.Select(tag => tag.Name));

        config.NewConfig<Post, ListedPostResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.Timestamp, src => src.CreatedDateTime.ToString("dd/MM/yy hh:mm:ss"))
            .Map(dest => dest.EditedTimestamp, src => src.UpdatedDateTime.ToString("dd/MM/yy hh:mm:ss"))
            .Map(dest => dest.Comments, src => src.CommentIds.Count)
            .Map(dest => dest.Tags, src => src.Tags.Select(tag => tag.Name));

        config.NewConfig<UpdatePostRequest, UpdatePostCommand>()
            .Map(dest => dest, src => src);
    }
}

