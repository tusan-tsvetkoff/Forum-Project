using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.Common.Enums;

public enum SortBy
{
    Newest,
    Oldest,
    MostPopular, // By likes? 
    LeastPopular, // By dislikes?
    MostCommented
}
