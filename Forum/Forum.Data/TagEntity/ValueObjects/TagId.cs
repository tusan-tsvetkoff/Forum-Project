using Forum.Data.Models.Identities;

namespace Forum.Data.TagEntity.ValueObjects;

public sealed class TagId : EntityId<Guid>
{
    private TagId(Guid value) : base(value)
    {
    }

    public static TagId CreateUnique()
    {
        return new TagId(Guid.NewGuid());
    }

    public static TagId Create(Guid value)
    {
        return new TagId(value);
    }
}
