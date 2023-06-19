using Forum.Data.Models.Identities;

namespace Forum.Data.UserAggregate.ValueObjects;

public sealed class UserId : AggregateRootId<Guid>
{
    private UserId(Guid value) : base(value)
    {
    }

   /* private UserId() : base(Guid.Empty)
    {
    }*/

    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }

    public static UserId Create(Guid userId)
    {
        return new UserId(userId);
    }
}