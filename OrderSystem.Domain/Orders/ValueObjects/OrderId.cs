using OrderSystem.Domain.Common.ValueObjects;

namespace OrderSystem.Domain.Orders.ValueObjects;

public sealed class OrderId : ValueObject
{
    public Guid Value { get; }

    private OrderId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("OrderId не может быть пустым", nameof(value));
        Value = value;
    }

    public static OrderId New() => new(Guid.NewGuid());

    public static OrderId FromGuid(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}