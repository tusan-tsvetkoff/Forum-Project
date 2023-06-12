using Forum.Data.Models;

namespace Forum.Data.PostAggregate.ValueObjects
{
    public sealed class Dislikes : ValueObject
    {
        public int Value { get; private set; }

        private Dislikes(int value)
        {
            Value = value;
        }

        public static Dislikes Create(int value)
        {
            return new Dislikes(value);
        }

        public Dislikes Increment() => new(Value + 1);

        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        private Dislikes()
        {

        }
    }
}
