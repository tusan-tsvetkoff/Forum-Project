using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Contracts.Common;
using Forum.Data.AuthorAggregate;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Application.Posts.Queries.ListPosts;

public class GetPostsQueryHandler :
    IRequestHandler<GetPostsQuery, ErrorOr<(List<Post>, PageInfo)>>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;

    public GetPostsQueryHandler(IPostRepository postRepository, IAuthorRepository authorRepository)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
    }
    public async Task<ErrorOr<(List<Post>, PageInfo)>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
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

        if(request.SortOrder == "desc")
        {
            postQuery = postQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            postQuery = postQuery.OrderBy(GetSortProperty(request));
        }

        var posts = await postQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var postCount = await postQuery.CountAsync(cancellationToken);

        HasPrevAndOrNextPage(request, postCount, out bool hasPreviousPage, out bool hasNextPage);

        var pageInfo = new PageInfo(
            request.Page,
            request.PageSize,
            postCount,
            hasNextPage,
            hasPreviousPage);

        return (posts, pageInfo);
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
