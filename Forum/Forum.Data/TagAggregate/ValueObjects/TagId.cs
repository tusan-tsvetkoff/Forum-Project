using Forum.Data.Models.Identities;

namespace Forum.Data.TagAggregate.ValueObjects;

public sealed class TagId : AggregateRootId<Guid>
{
    private TagId(Guid value) : base(value)
    {
    }

/*    private TagId() : base(Guid.Empty)
    {
    }*/

    public static TagId CreateUnique()
    {
        return new TagId(Guid.NewGuid());
    }

    public static TagId Create(Guid value)
    {
        return new TagId(value);
    }
}
