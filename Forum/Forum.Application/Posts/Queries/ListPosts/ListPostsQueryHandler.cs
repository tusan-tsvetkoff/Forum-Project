using ErrorOr;
using Forum.Application.Common.Enums;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using Forum.Data.PostAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using MediatR;


namespace Forum.Application.Posts.Queries.ListPosts
{
    public class ListPostsQueryHandler : IRequestHandler<ListPostsQuery, ErrorOr<(List<Post>, int TotalPosts)>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUserRepository _userRepository;

        public ListPostsQueryHandler(IPostRepository postRepository, IAuthorRepository authorRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<(List<Post>, int TotalPosts)>> Handle(ListPostsQuery query, CancellationToken cancellationToken)
        {
            /// TODO: Move to validator
            if (!Enum.GetNames(typeof(SortBy))
                .Any(x => x
                .ToString()
                .Equals(query.Sort, StringComparison.OrdinalIgnoreCase)) && query.Sort is not null)
            {
                return Errors.Post.InvalidSort;
            }

            var user = await _userRepository.GetUserByUsername(query.Username);
            var author = await _authorRepository.GetByUserIdAsync(UserId.Create(user.Id.Value));

            // Pagination
            int skipCount = (query.Page - 1) * query.PageSize;

            string? sortBy = query.Sort.ToLower();

            return await _postRepository.GetAllPostsAsync(
                sortBy, 
                query.Search, 
                AuthorId.Create(author?.Id.Value!),
                query.Page, query.PageSize);
        }
    }
}
