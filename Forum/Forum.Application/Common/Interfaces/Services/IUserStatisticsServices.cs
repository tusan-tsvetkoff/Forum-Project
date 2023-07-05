﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Common.Interfaces.Services;

public interface IUserStatisticsServices
{
    Task<int> GetTotalUsersCountAsync();
}
