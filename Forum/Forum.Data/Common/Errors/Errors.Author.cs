using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Common.Errors;

public static partial class Errors
{
    public static class Author
    {
        public static Error UsernameExists => Error.Validation(
            code: "Error.Author.Username.Exists",
            description: "Username already exists.");
    }
}
