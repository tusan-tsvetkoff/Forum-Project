using Forum.Data.Models;

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
