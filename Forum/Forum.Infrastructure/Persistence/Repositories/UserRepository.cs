using Forum.Application.Common.Interfaces.Persistence;
using Forum.Data.UserAggregate;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(UserId userId)
        {
            return _users.SingleOrDefault(u => u.Id == userId);
        }

        public void Update(User user)
        {
            _users[_users.FindIndex(u => u.Id == user.Id)] = user;
        }
    }
}
