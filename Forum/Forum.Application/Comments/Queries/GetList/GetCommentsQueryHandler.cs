using ErrorOr;
using Forum.Application.Comments.Common;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Contracts.Common;
using Forum.Data.CommentAggregate;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Forum.Contracts.Post;

namespace Forum.Application.Comments.Queries.GetList;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, ErrorOr<(List<CommentResult>, PageInfo)>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public GetCommentsQueryHandler(ICommentRepository commentRepository,
                                   IAuthorRepository authorRepository, IPostRepository postRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _authorRepository = authorRepository;
    }
    public async Task<ErrorOr<(List<CommentResult>, PageInfo)>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        // 1. Get comments as queryable
        var commentsQuery = await _commentRepository.GetCommentsAsync();
        var postId = PostId.Create(request.PostId);
        // 2. Apply all the shit
        // 2.5 Check if post exists
        if (await _postRepository.PostExistsAsync(postId) is false)
        {
            return Errors.Post.NotFound;
        }

        commentsQuery = commentsQuery.Where(c
            => c.PostId == postId);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            commentsQuery = commentsQuery.Where(c =>
                c.Content.Contains(request.SearchTerm));
        }

        if (request.SortOrder == "desc")
        {
            commentsQuery = commentsQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            commentsQuery = commentsQuery.OrderBy(GetSortProperty(request));
        }

        int page = request.Page == 0 ? 1 : request.Page;

        int pageSize = request.PageSize == 0 ? 10 : request.PageSize;

        var comments = await commentsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var commentCount = await commentsQuery.CountAsync(cancellationToken);

        HasPrevAndOrNextPage(request, commentCount, out bool hasPreviousPage, out bool hasNextPage);

        var pageInfo = new PageInfo(
            page,
            pageSize,
            commentCount,
            hasPreviousPage,
            hasNextPage);
        // 3. Something important as well
        var responseList = MapPostResponseList(comments);

        // 4. Return that something important kekw
        return (responseList, pageInfo);
    }

    private List<CommentResult> MapPostResponseList(List<Comment> comments)
    {
        return comments.Select(c =>
        {
            var username = _authorRepository.GetByAuthorIdAsync(c.AuthorId).Result!.Username!; ;
            return new CommentResult(
                c.Id.Value.ToString(),
               c.Content,
                new AuthorResponse(
                    c.AuthorId.Value,
                    username),
                c.CreatedAt.ToString("dd-MM-yy hh:mm:ss"),
                c.UpdatedAt?.ToString("dd-MM-yy hh:mm:ss"));
        }).ToList();
    }

    private static Expression<Func<Comment, object>> GetSortProperty(GetCommentsQuery query)
    {
        return query.SortColumn?.ToLower() switch
        {
            "content" => c => c.Content,
            "created" => c => c.CreatedAt,
            _ => p => p.CreatedAt
        };
    }

    private static void HasPrevAndOrNextPage(GetCommentsQuery request, int commentCount, out bool hasPreviousPage, out bool hasNextPage)
    {
        hasPreviousPage = request.Page > 1;
        hasNextPage = request.Page * request.PageSize < commentCount;
    }
}
