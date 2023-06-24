using ErrorOr;
using Forum.Contracts.Common;
using Forum.Data.UserAggregate;
using MediatR;

namespace Forum.Application.Users.Queries;

public record GetUsersQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int? Page,
    int? PageSize) : IRequest<ErrorOr<(List<User>, PageInfo)>>;