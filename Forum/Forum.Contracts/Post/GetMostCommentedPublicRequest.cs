using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Contracts.Post;

public record GetMostCommentedPublicRequest(
    string MostCommented = "mostcommented",
    int PageSize = 10,
    int PageCount = 1);