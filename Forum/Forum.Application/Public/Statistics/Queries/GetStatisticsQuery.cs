using ErrorOr;
using Forum.Contracts.Statistics;
using MediatR;

namespace Forum.Application.Public.Statistics.Queries;

public record GetStatisticsQuery(
    bool GetStats) : IRequest<ErrorOr<StatisticsResponse>>;
