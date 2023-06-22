using ErrorOr;
using Forum.Application.Common.Interfaces.Persistence;
using Forum.Contracts.Common;
using Forum.Data.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Application.Users.Queries;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ErrorOr<(List<User>, PageInfo)>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<(List<User>, PageInfo)>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<User> usersQuery = _userRepository.GetAllUsersAsync();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            usersQuery = usersQuery.Where(u =>
            u.Username.Contains(request.SearchTerm) ||
            u.FirstName.Contains(request.SearchTerm) ||
            u.LastName.Contains(request.SearchTerm));
        }

        if (request.SortOrder == "desc")
        {
            usersQuery = usersQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            usersQuery = usersQuery
                .OrderBy(GetSortProperty(request));
        }

        var users = await usersQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var userCount = await usersQuery.CountAsync(cancellationToken);

        HasPrevAndOrNextPage(request, userCount, out bool hasPreviousPage, out bool hasNextPage);

        var pageInfo = new PageInfo
        (
            request.Page,
            request.PageSize,
            userCount,
            hasNextPage,
            hasPreviousPage
        );

        return (users, pageInfo);
    }

    private static void HasPrevAndOrNextPage(GetUsersQuery request, int userCount, out bool hasPreviousPage, out bool hasNextPage)
    {
        hasPreviousPage = request.Page > 1;
        hasNextPage = request.Page * request.PageSize < userCount;
    }

    private static Expression<Func<User, object>> GetSortProperty(GetUsersQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "username" => u => u.Username,
            "firstName" => u => u.FirstName,
            "lastName" => u => u.LastName,
            _ => u => u.CreatedDate // questionable
        };
    }
}
