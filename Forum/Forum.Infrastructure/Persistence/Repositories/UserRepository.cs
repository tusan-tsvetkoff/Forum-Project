using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.AuthorAggregate;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
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

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(UserId userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<User?> GetUserByUsernameAsyc(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);

            if (await _dbContext.Author.FirstOrDefaultAsync(author => author.UserId == user.Id) is Author author) 
            {
                _dbContext.Author.Remove(author);
            }
        }

        // This is a bit weird. I'm not sure if I should be updating the Author entity here.
        public async void Update(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.Author.Update(_dbContext.Author.FirstOrDefault(author => author.UserId == user.Id)!);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<User> GetAllUsersAsync()
        {
            return _dbContext.Users;
        }
    }
}
