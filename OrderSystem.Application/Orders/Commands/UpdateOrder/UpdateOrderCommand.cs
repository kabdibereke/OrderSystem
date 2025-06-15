using MediatR;

namespace OrderSystem.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<Guid>
{
    public Guid OrderId { get; set; }

    public List<UpdateOrderItemDto> Items { get; set; } = new();
    
}

public class UpdateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}