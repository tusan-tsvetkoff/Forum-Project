using Forum.Data.Models;
using Forum.Data.TagEntity.ValueObjects;

namespace Forum.Data.TagEntity;

public sealed class Tag : Entity<TagId>
{
    public string Name { get; private set; }

#pragma warning disable S4136
    private Tag(string name) : base(TagId.CreateUnique())
    {
        Name = name;
    }
#pragma warning restore S4136

    public static Tag Create(string name)
    {
        // enforce invariants later
        var tag = new Tag(name);
        return tag;
    }
#pragma warning disable CS8618
    private Tag()
    {
    }
#pragma warning restore CS8618
}
