using MediatR;
using OrderSystem.Domain.Orders.Repositories;
using OrderSystem.Domain.Orders.ValueObjects;

namespace OrderSystem.Application.Orders.Queries.GetByIdOrder;

public class GetByIdOrderHandler: IRequestHandler<GetByIdOrderQuery, OrderByIdDto>
{
    private readonly IOrderRepository _orderRepository;
    
    public GetByIdOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<OrderByIdDto> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.FromGuid(request.Id);
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        if (order is null)
        {
            throw new InvalidOperationException($"Заказ с Id={request.Id} не найден.");
        }

        var orderItems = order.Items.Select(x => new OrderItemDto()
        {
            ProductId = x.ProductId.Value,
            ProductName = x.ProductName,
            Quantity = x.Quantity,
            UnitPrice = x.UnitPrice
        }).ToList();


        return new OrderByIdDto()
        {
            Id = order.Id.Value,
            CustomerId = order.CustomerId.Value,
            Items = orderItems
        };

    }
}