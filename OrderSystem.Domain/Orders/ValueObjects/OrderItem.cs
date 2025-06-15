using OrderSystem.Domain.Catalog.ValueObjects;
using OrderSystem.Domain.Common.ValueObjects;

namespace OrderSystem.Domain.Orders.ValueObjects;

public sealed class OrderItem : ValueObject
{
    public ProductId ProductId { get; }
    public string ProductName { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public decimal Total => UnitPrice * Quantity;

    public OrderItem(ProductId productId, string productName, int quantity, decimal unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть больше нуля");

        if (unitPrice < 0)
            throw new ArgumentException("Цена не может быть отрицательной");

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ProductId;
        yield return ProductName;
        yield return Quantity;
        yield return UnitPrice;
    }
}