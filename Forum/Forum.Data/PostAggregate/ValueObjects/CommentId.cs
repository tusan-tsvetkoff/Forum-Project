using Forum.Data.Models.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.PostAggregate.ValueObjects;

public sealed class CommentId : EntityId<Guid>
{
    private CommentId(Guid value) : base(value)
    {
    }

    public CommentId()
    {
    }

    public static CommentId CreateUnique()
    {
        return new CommentId(Guid.NewGuid());
    }

    public static CommentId Create(Guid value)
    {
        return new CommentId(value);
    }
}
