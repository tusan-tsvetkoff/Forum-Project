namespace Forum.Data.Models.Identities;

public abstract class EntityId<TId> : ValueObject
{
    public TId Value { get; }

    protected EntityId(TId value)
    {
        Value = value;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string? ToString() => Value?.ToString() ?? base.ToString();

    protected EntityId()
    {
    }
}