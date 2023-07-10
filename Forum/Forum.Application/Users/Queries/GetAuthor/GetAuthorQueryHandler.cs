using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using MediatR;

namespace Forum.Application.Users.Queries.GetUser;

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, ErrorOr<Author>>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    public async Task<ErrorOr<Author>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        var authorId = AuthorId.Create(request.AuthorId);

        var author = await _authorRepository.GetByAuthorIdAsync(authorId);
        if(author is null)
        {
            return Errors.Author.NotFound;
        }

        return author;
    }
}
