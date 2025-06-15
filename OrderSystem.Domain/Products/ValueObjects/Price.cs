using OrderSystem.Domain.Common.ValueObjects;

namespace OrderSystem.Domain.Catalog.ValueObjects;

public sealed class Price : ValueObject
{
    public decimal Value { get; }

    public Price(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Цена не может быть отрицательной", nameof(value));

        Value = value;
    }

    public static Price Zero => new(0m);

    public Price Add(Price other) => new(Value + other.Value);

    public Price Multiply(int quantity) => new(Value * quantity);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString("F2");
}