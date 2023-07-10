using Forum.Application.Common.Interfaces.Persistence;
using Forum.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Services;

public class UserStatisticsServices : IUserStatisticsServices
{ 
    private readonly IUserRepository _userRepository;

    public UserStatisticsServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<int> GetTotalUsersCountAsync()
    {

        return await _userRepository.GetTotalUsersCountAsync();
    }
}
