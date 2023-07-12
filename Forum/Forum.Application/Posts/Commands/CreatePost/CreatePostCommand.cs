using ErrorOr;
using Forum.Data.PostAggregate;
using MediatR;

namespace Forum.Application.Posts.Commands.CreatePost;

public record CreatePostCommand(
    Guid UserId,
    string Title,
    string Content,
    List<string> Tags) : IRequest<ErrorOr<Post>>;
