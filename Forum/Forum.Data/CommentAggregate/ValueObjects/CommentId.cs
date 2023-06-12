using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.Comment.ValueObjects
{
    public sealed class CommentId : ValueObject
    {
        public Guid Value { get; }

        private CommentId(Guid value)
        {
            Value = value;
        }

        public static CommentId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
