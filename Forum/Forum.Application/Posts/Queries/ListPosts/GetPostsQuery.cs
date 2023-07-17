using ErrorOr;
using Forum.Contracts.Common;
using Forum.Contracts.Post;
using MediatR;

namespace Forum.Application.Posts.Queries.ListPosts;

public record GetPostsQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int? Page,
    int? PageSize,
    string? Username,
    string? Tags) : IRequest<ErrorOr<(List<ListedPostResponse>, PageInfo)>>;
