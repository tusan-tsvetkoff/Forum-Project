using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Contracts.Post;
using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using MediatR;

namespace Forum.Application.Users.Queries.GetUser;

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, ErrorOr<AuthorResponse>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUserRepository _userRepository;
    public GetAuthorQueryHandler(IAuthorRepository authorRepository, 
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _authorRepository = authorRepository;
    }
    public async Task<ErrorOr<AuthorResponse>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        var authorId = AuthorId.Create(request.AuthorId);

        var author = await _authorRepository.GetByAuthorIdAsync(authorId);
        if(author is null)
        {
            return Errors.Author.NotFound;
        }
        var user = await _userRepository.GetUserByUsernameAsyc(author.Username);

        var authorResponse = new AuthorResponse(
            author.Id.ToString(),
            $"{user.FirstName} {user.LastName}",
            user.Email,
            author.Username
        );

        return authorResponse;
    }
}
