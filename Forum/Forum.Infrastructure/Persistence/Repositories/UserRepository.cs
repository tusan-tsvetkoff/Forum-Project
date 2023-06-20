using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        private readonly ForumDbContext _dbContext;

        public UserRepository(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Should this be AddAsync or CreateAsync? I'm not sure.
        public async Task AddAsync(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(UserId userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        // I guess this will also delete authors (currently also deleting all related posts, might change that later (somehow))
        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);

            if (await _dbContext.Author.FirstOrDefaultAsync(author => author.UserId == user.Id) is Author author) 
            {
                _dbContext.Author.Remove(author);
            }
        }

        // Deprecated (will probably not be used **ever**) - I'm keeping it here just in case.
        public void Update(User user)
        {
            _users[_users.FindIndex(u => u.Id == user.Id)] = user;
        }
    }
}
