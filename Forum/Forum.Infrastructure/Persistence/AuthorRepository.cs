using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;

namespace Forum.Infrastructure.Persistence;

public class AuthorRepository : IAuthorRepository
{
    private static readonly List<Author> _authors = new();

    private readonly ForumDbContext _dbContext;

    public AuthorRepository(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Author Add(Author author)
    {
        _authors.Add(author);
        return author;
    }

    public Author? GetByAuthorId(string authorId)
    {
       return _authors.SingleOrDefault(a => a.Id.Value == authorId);
    }

    public Author? GetByUserId(Guid userId)
    {
        return _authors.SingleOrDefault(a => a.UserId.Value == userId);
    }

    public Author? GetByUsername(string username)
    {
        return _authors.SingleOrDefault(a => a.Username == username);
    }
}
