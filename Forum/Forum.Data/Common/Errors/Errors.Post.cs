﻿using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Common.Errors
{
    public static partial class Errors
    {
        public static class Post
        {
            public static Error TitleLength => Error.Validation(
                description: "Title must be between 4 and 32 symbols.",
                code: "Post.TitleLength");
            public static Error DescriptionLength => Error.Validation(
                description: "Content must be between 32 and 8192 symbols.");

        }
    }
}