using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Contracts.Common;
using Forum.Contracts.Post;
using Forum.Data.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Application.Posts.Queries.ListPosts;

public class GetPostsQueryHandler :
    IRequestHandler<GetPostsQuery, ErrorOr<(List<ListedPostResponse>, PageInfo)>>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;

    public GetPostsQueryHandler(IPostRepository postRepository, IAuthorRepository authorRepository)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
    }
    public async Task<ErrorOr<(List<ListedPostResponse>, PageInfo)>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var postQuery = _postRepository.GetPosts();

        var author = request.Username is not null
            ? await _authorRepository.GetByUsernameAsync(request.Username)
            : null;

        postQuery = author is not null ?
            postQuery.Where(x => x.AuthorId == author.Id)
            : postQuery;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            postQuery = postQuery.Where(p =>
                p.Title.Contains(request.SearchTerm) ||
                p.Content.Contains(request.SearchTerm));
        }

        if (request.SortOrder == "desc")
        {
            postQuery = postQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            postQuery = postQuery.OrderBy(GetSortProperty(request));
        }

        int page = request.Page ?? 1;
        int pageSize = request.PageSize ?? 10;

        var posts = await postQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var postCount = await postQuery.CountAsync(cancellationToken);

        HasPrevAndOrNextPage(request, postCount, out bool hasPreviousPage, out bool hasNextPage);

        // Just for visual purposes if the page size is bigger than the post count
        pageSize = pageSize > postCount ? postCount : pageSize;

        var pageInfo = new PageInfo(
            page,
            pageSize,
            postCount,
            hasNextPage,
            hasPreviousPage);

        // Serious refactoring needed here
        List<ListedPostResponse> postResponseTasks = MapPostResponseList(posts);

        return (postResponseTasks, pageInfo);
    }

    private List<ListedPostResponse> MapPostResponseList(List<Post> posts)
    {
        return posts.Select(p =>
        {
            var username = _authorRepository.GetByAuthorIdAsync(p.AuthorId)!.Result!.Username;
            return new ListedPostResponse(
                p.Id.Value.ToString(),
                new AuthorResponse(
                    p.AuthorId.Value,
                    username),
                p.Title,
                p.Content,
                new Likes(p.Likes.Value),
                new Dislikes(p.Dislikes.Value),
                p.CommentIds.Count,
                p.CreatedDateTime.ToString("dd/MM/yy hh:mm:ss"),
                p.UpdatedDateTime.ToString("dd/MM/yy hh:mm:ss"));
        }).ToList();
    }

    private static void HasPrevAndOrNextPage(GetPostsQuery request, int postCount, out bool hasPreviousPage, out bool hasNextPage)
    {
        hasPreviousPage = request.Page > 1;
        hasNextPage = request.Page * request.PageSize < postCount;
    }


    private static Expression<Func<Post, object>> GetSortProperty(GetPostsQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "title" => p => p.Title,
            "content" => p => p.Content,
            "created" => p => p.CreatedDateTime,
            _ => p => p.CreatedDateTime
        };
    }
}
