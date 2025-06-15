namespace OrderSystem.Application.Orders.Queries.GetAllOrder;

public class AllOrderDto
{
  public Guid Id { get; set; }
  public Guid CustomerId  { get; set; }
  public string OrderStatus { get; set; }
}