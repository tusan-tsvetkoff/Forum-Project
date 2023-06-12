using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.PostAggregate;
using MediatR;
using Forum.Data.AuthorAggregate;

namespace Forum.Application.Posts.Queries.ListPosts
{
    public class ListPostsQueryHandler : IRequestHandler<ListPostsQuery, ErrorOr<List<Post>>>
    {
        private readonly IPostRepository _postRepository;

        public ListPostsQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<ErrorOr<List<Post>>> Handle(ListPostsQuery request, CancellationToken cancellationToken)
        {
            var authorId = AuthorId.Create(request.AuthorId);

            return await _postRepository.ListAsync(authorId);
        }
    }
}
