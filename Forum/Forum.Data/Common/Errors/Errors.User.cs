using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            description: "Email is already in use.",
            code: "User.DuplicateEmail");
        public static Error NotFound => Error.NotFound(
            description: "User was not found.",
            code: "User.NotFound");
        public static Error FullNameLength => Error.Validation(
            description: "First and Last name must be between 4 and 32 symbols. Please try again.",
            code: "User.FullNameLength");
        public static Error InvalidEmail => Error.Validation(
            description: "Invalid email format. \nPlease provide a valid email address.",
            code: "User.InvalidEmail");
        public static Error PasswordLength => Error.Validation(
            description: "Password is too short. \nPlease enter a password with at least 15 symbols.",
            code: "User.PasswordLength");

        public static Error DeleteFailed => Error.Unexpected(
                description: "User could not be deleted.",
                code: "User.DeleteFailed");

        public static Error UsernameExists => Error.Conflict(
            description: "Username is already in use.",
            code: "User.UsernameExists");
    }
}
