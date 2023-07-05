using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Statistics;

public record StatisticsResponse(
    int TotalPostsCount,
    int TotalUsersCount);

