using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Common.Errors.CustomErrors;

public static class MyErrors
{
    public static Error Forbidden(string code, string description)
    {
        return Error.Custom(type: CustomErrorTypes.Forbidden, code: code, description: description);
    }
}
