using ErrorOr;
using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Commenter.ValueObjects;

public sealed class CommenterId : ValueObject
{
    public Guid Value { get; }

    private CommenterId(Guid value)
    {
        Value = value;
    }

    public static CommenterId CreateUnique()
    {
        return new CommenterId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;

    }
}
