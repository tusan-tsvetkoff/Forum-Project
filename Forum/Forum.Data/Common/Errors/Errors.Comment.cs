using ErrorOr;
using Forum.Data.Common.Errors.CustomErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Common.Errors
{
    public static partial class Errors
    {
        public static class Comment
        {
            public static Error NotFound => Error.NotFound(
                description: "Comment not found.",
                code: "Comment.NotFound");

            public static Error NotOwner => MyErrors.Forbidden(
                description: "You are not the owner of this comment.", // placeholder
                code: "Comment.NotOwner");
                

        }

    }
}
