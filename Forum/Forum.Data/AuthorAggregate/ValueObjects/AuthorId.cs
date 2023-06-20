using Forum.Data.Models.Identities;
using Forum.Data.UserAggregate.ValueObjects;

namespace Forum.Data.AuthorAggregate.ValueObjects;

public sealed class AuthorId : AggregateRootId<string>
{
    private AuthorId(string value) : base(value)
    {
    }

    public static AuthorId CreateUnique(UserId userId)
    {
        return new AuthorId($"Author_{userId.Value}");
    }

    public static AuthorId Create(string authorId)
    {
        return new AuthorId(authorId);
    }
}
