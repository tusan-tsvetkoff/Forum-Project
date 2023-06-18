using Forum.Data.Models.Identities;

namespace Forum.Data.PostAggregate.ValueObjects;

public sealed class PostId : AggregateRootId<Guid>
{
    private PostId(Guid value) : base(value)
    {
    }

    private PostId() : base(Guid.Empty)
    {
    }

    public static PostId CreateUnique()
    {
        return new PostId(Guid.NewGuid());
    }

    public static PostId Create(Guid value)
    {
        return new PostId(value);
    }
}
