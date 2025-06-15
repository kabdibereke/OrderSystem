using OrderSystem.Domain.Common.Entities;
using OrderSystem.Domain.Customers.ValueObjects;
using OrderSystem.Domain.Orders.ValueObjects;

namespace OrderSystem.Domain.Orders.Entities;

public sealed class Order : Entity<OrderId>
{
    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public OrderStatus Status { get; private set; }

    public CustomerId CustomerId { get; }

    public DateTime CreatedAt { get; }

    public Order(OrderId id, CustomerId customerId) : base(id)
    {
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Created;
    }

    public void AddItem(OrderItem item)
    {
        _items.Add(item);
    }

    public decimal GetTotalPrice()
    {
        return _items.Sum(i => i.Total);
    }

    public void Confirm()
    {
        if (Status.Value != OrderStatus.Created.Value)
            throw new InvalidOperationException("Только новый заказ можно подтвердить");

        Status = OrderStatus.Confirmed;
    }

    public void Ship()
    {
        if (Status.Value != OrderStatus.Confirmed.Value)
            throw new InvalidOperationException("Только подтвержденный заказ можно отгрузить");

        Status = OrderStatus.Shipped;
    }

    public void Cancel()
    {
        if (Status.Value == OrderStatus.Shipped.Value)
            throw new InvalidOperationException("Нельзя отменить отгруженный заказ");

        Status = OrderStatus.Cancelled;
    }
}