using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.Common.Errors;
using Forum.Data.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Persistence;

public class AuthorRepository : IAuthorRepository
{
    private static readonly List<Author> _authors = new();

    private readonly ForumDbContext _dbContext;

    public AuthorRepository(ForumDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddАsync(Author author)
    {
        _dbContext.Add(author);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Author?> GetByAuthorIdAsync(AuthorId authorId)
    {
       return await _dbContext.Author.FirstOrDefaultAsync(a => a.Id == authorId);
    }

    public async Task<Author?> GetByUserIdAsync(UserId userId)
    {
        return await _dbContext.Author.FirstOrDefaultAsync(a => a.UserId == userId);
    }

    public async Task<Author?> GetByUsernameAsync(string username)
    {
        return await _dbContext.Author.FirstOrDefaultAsync(a => a.Username == username);
    }

    public async Task UpdateAsync(Author author)
    {
        _dbContext.Author.Update(author);
        await _dbContext.SaveChangesAsync();
    }

    private bool Exists(string username)
    {
        return _authors.Any(a => a.Username == username);
    }
}
