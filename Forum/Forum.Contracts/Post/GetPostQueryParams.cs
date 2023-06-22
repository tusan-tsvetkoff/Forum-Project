using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Post;

public record GetPostsQueryParams(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    string? Username,
    int Page,
    int PageSize);
