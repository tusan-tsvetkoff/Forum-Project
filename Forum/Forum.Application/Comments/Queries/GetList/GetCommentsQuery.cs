using ErrorOr;
using Forum.Application.Comments.Common;
using Forum.Contracts.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Comments.Queries.GetList;

public record GetCommentsQuery(
    Guid PostId,
    string? SearchTerm,
    string? SortOrder,
    string? SortColumn,
    int Page,
    int PageSize) : IRequest<ErrorOr<(List<CommentResult>, PageInfo)>>;