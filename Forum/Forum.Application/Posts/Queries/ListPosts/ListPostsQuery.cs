using ErrorOr;
using Forum.Data.PostAggregate;
using MediatR;

namespace Forum.Application.Posts.Queries.ListPosts;

public record ListPostsQuery(string AuthorId) : IRequest<ErrorOr<List<Post>>>;
