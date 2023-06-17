using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Post;

public record ListPostsRequest(
    string Sort,
    string Username,
    int Page,
    int PageSize,
    string Search);
