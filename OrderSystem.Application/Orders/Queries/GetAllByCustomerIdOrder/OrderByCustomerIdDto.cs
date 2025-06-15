namespace OrderSystem.Application.Orders.Queries.GetAllByCustomerIdOrder;

public class OrderByCustomerIdDto
{
    public Guid Id { get; set; }
    public Guid CustomerId  { get; set; }
    public string OrderStatus { get; set; }
}
