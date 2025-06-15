using OrderSystem.Domain.Common.ValueObjects;

namespace OrderSystem.Domain.Catalog.ValueObjects;

public sealed class ProductId : ValueObject
{
    public Guid Value { get; }

    private ProductId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("ProductId не может быть пустым", nameof(value));

        Value = value;
    }

    public static ProductId New() => new(Guid.NewGuid());

    public static ProductId FromGuid(Guid value) => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}