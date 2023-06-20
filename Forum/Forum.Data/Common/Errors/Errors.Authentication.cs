using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            description: "Login credentials are wrong.",
            code: "Authorization.InvalidCredentials");
        public static Error UnauthorizedAction => Error.Unexpected(
            description: "Please log in or register to continue.",
            code: "Authentication.UnauthorizedAction");
        // Probably deprecated
        public static Error InvalidGuid => Error.Unexpected(
            description: "ID did not match correct type.",
            code: "Authentication.InvalidGuid");
    }
}
