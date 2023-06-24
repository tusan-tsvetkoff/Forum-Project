using ErrorOr;
using Forum.Contracts.Common;
using Forum.Data.PostAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Posts.Queries.ListPosts;

public record GetPostsQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int? Page,
    int? PageSize,
    string? Username) : IRequest<ErrorOr<(List<Post>, PageInfo)>>;
