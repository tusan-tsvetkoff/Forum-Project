using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using MediatR;

namespace Forum.Application.Posts.Queries.GetPost;

public class GetPostQuerryHandler : IRequestHandler<GetPostQuery, ErrorOr<Post>>
{
    private readonly IPostRepository _postRepository;

    public GetPostQuerryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<ErrorOr<Post>> Handle(GetPostQuery query, CancellationToken cancellationToken)
    {
        return await _postRepository.GetByIdAsync(PostId.Create(query.PostId));
    }
}
