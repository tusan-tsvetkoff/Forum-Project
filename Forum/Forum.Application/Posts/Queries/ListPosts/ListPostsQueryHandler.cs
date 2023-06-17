using ErrorOr;
using Forum.Application.Common.Enums;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate;
using MediatR;
using System.Collections.Immutable;

namespace Forum.Application.Posts.Queries.ListPosts
{
    public class ListPostsQueryHandler : IRequestHandler<ListPostsQuery, ErrorOr<List<Post>>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;

        public ListPostsQueryHandler(IPostRepository postRepository, IAuthorRepository authorRepository)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
        }

        public async Task<ErrorOr<List<Post>>> Handle(ListPostsQuery request, CancellationToken cancellationToken)
        {
            // TODO: Gotta fix this somehow, not sure how yet
            Author author = null;
            if (request.Username != null)
            {
                author = _authorRepository.GetByUsername(request.Username);
                if (author is null)
                {
                    return Errors.User.NotFound;
                }
            }
            // Check if request.Sort is valid within the SortBy enum
            // Also, lowercase/uppercase doesn't matter

            if (!Enum.GetNames(typeof(SortBy))
                .Any(x => x
                .ToString()
                .Equals(request.Sort, StringComparison.OrdinalIgnoreCase)) && request.Sort is not null)
            {
                return Errors.Post.InvalidSort;
            }

            // TODO: Fix this..?
            var authorId = author?.Id?.Value;
            return await _postRepository.GetPostsAsync(request.Sort, authorId, request.Page, request.PageSize, request.Search);
        }
    }
}
