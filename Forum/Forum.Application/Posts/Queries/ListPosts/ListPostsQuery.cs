using ErrorOr;
using Forum.Data.PostAggregate;
using MediatR;

namespace Forum.Application.Posts.Queries.ListPosts;

public record ListPostsQuery(
    string Sort,
    string Username,
    int Page,
    int PageSize,
    string Search) : IRequest<ErrorOr<(List<Post>, int TotalPosts)>>;
