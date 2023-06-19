using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;

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
        public void Add(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }

        public User? GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetUserById(UserId userId)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Id == userId);
        }

        public void Update(User user)
        {
            _users[_users.FindIndex(u => u.Id == user.Id)] = user;
        }
    }
}
