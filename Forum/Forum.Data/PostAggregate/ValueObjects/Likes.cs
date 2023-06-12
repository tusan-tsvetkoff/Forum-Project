using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.PostAggregate.ValueObjects
{
    public sealed class Likes : ValueObject
    {
        public int Value { get; private set; }

        private Likes(int value)
        {
            Value = value;
        }

        public static Likes Create(int value)
        {
            return new Likes(value);
        }

        public Likes Increment() => new(Value + 1);

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        private Likes()
        {

        }
    }
}
