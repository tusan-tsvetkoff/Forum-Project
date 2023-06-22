using Forum.Data.UserAggregate.ValueObjects;
using Forum.Data.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmail(string email);
    Task AddAsync(User user);
    void Update(User user);
    Task<User?> GetUserByIdAsync(UserId userId);
    Task DeleteAsync(User user);
    Task<User?> GetUserByUsername(string username);
    IQueryable<User> GetAllUsersAsync();
}
