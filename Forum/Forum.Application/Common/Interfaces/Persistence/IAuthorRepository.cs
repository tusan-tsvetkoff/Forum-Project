using Forum.Data.AuthorAggregate;
using Forum.Data.AuthorAggregate.ValueObjects;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Application.Common.Interfaces.Persistence;

public interface IAuthorRepository
{
    Task AddАsync(Author author);
    Task<Author?>? GetByAuthorIdAsync(AuthorId authorId);
    Task<Author?> GetByUserIdAsync(UserId userId);
    Task<Author?> GetByUsernameAsync(string username);
    Task UpdateAsync(Author author);
}
