using ErrorOr;

namespace Forum.Data.Common.Errors;

public static partial class Errors
{
    public static class Tag
    {
        public static Error TagNotFound(Guid id) =>
            Error.NotFound(
                               code: "tag_not_found",
                                              description: $"Tag with id {id} not found.");

        public static Error TagAlreadyExists(string name) =>
            Error.Conflict(
                               code: "tag_already_exists",
                                description: $"Tag with name {name} already exists.");
    }
}
