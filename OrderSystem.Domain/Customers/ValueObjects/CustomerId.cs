using OrderSystem.Domain.Common.ValueObjects;

namespace OrderSystem.Domain.Customers.ValueObjects;

public sealed class CustomerId : ValueObject
{
    public Guid Value { get; }

    private CustomerId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("CustomerId не может быть пустым", nameof(value));

        Value = value;
    }

    public static CustomerId New() => new(Guid.NewGuid());

    public static CustomerId FromGuid(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}