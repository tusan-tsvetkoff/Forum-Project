using Forum.Application.Common.Interfaces.Persistence;
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
        public async Task AddAsync(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public User? GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(UserId userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public void Update(User user)
        {
            _users[_users.FindIndex(u => u.Id == user.Id)] = user;
        }
    }
}
