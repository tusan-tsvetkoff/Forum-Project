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
    User? GetUserByEmail(string email);
    void Add(User user);
    void Update(User user);
    User? GetUserById(UserId userId);
}
