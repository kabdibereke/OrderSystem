using OrderSystem.Domain.Common.ValueObjects;

namespace OrderSystem.Domain.Orders.ValueObjects;

public sealed class TotalPrice : ValueObject
{
    public decimal Value { get; }

    public TotalPrice(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Итоговая сумма не может быть отрицательной");
        Value = value;
    }

    public TotalPrice Add(TotalPrice other) => new(Value + other.Value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString("F2");
}