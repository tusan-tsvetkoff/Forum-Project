using ErrorOr;
using Forum.Application.Common.Interfaces.Services;
using Forum.Application.Services;
using Forum.Contracts.Statistics;
using Forum.Data.Common.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Public.Statistics.Queries;

public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, ErrorOr<StatisticsResponse>>
{
    private readonly IPostStatisticsService _postStatisticsService;
    private readonly IUserStatisticsServices _userStatisticsServices;

    public GetStatisticsQueryHandler(IPostStatisticsService postStatisticsService, IUserStatisticsServices userStatisticsServices)
    {
        _postStatisticsService = postStatisticsService;
        _userStatisticsServices = userStatisticsServices;
    }
    public async Task<ErrorOr<StatisticsResponse>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
    {
        if (request.GetStats)
        {
            var totalPostsCount = await _postStatisticsService.GetTotalPostsCountAsync();
            var totalUsersCount = await _userStatisticsServices.GetTotalUsersCountAsync();
            return new StatisticsResponse(totalPostsCount, totalUsersCount);
        }
        else
        {
            return Errors.Authentication.UnauthorizedAction;
        }
    }
}
