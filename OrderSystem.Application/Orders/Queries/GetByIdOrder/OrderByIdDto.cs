namespace OrderSystem.Application.Orders.Queries.GetByIdOrder;

public class OrderByIdDto
{
    public Guid Id { get; set; }
    public Guid CustomerId  { get; set; }
    public string OrderStatus { get; set; }
    public List<OrderItemDto>  Items { get; set; }
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}