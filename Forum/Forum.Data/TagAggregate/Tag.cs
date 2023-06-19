using Forum.Data.Models;
using Forum.Data.PostAggregate;
using Forum.Data.PostAggregate.ValueObjects;
using Forum.Data.TagAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.TagAggregate;

public sealed class Tag : AggregateRoot<TagId, Guid>
{
    public string Name { get; private set; }

    private Tag(TagId tagId, string name) : base(tagId)
    {
        Name = name;
    }

    public static Tag Create(string name)
    {
        var tag = new Tag(TagId.CreateUnique(), name);
        return tag;
    }
#pragma warning disable CS8618
    private Tag()
    {
    }
#pragma warning restore CS8618
}
