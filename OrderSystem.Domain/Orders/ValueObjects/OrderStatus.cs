using OrderSystem.Domain.Common.ValueObjects;

namespace OrderSystem.Domain.Orders.ValueObjects;

public sealed class OrderStatus : ValueObject
{
    public static readonly OrderStatus Created = new("Created");
    public static readonly OrderStatus Confirmed = new("Confirmed");
    public static readonly OrderStatus Shipped = new("Shipped");
    public static readonly OrderStatus Cancelled = new("Cancelled");

    public string Value { get; }

    private OrderStatus(string value)
    {
        Value = value;
    }

    public static OrderStatus From(string status)
    {
        return status switch
        {
            "Created" => Created,
            "Confirmed" => Confirmed,
            "Shipped" => Shipped,
            "Cancelled" => Cancelled,
            _ => throw new ArgumentException($"Unknown order status: {status}")
        };
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}