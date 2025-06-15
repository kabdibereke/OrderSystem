using MediatR;
using OrderSystem.Domain.Orders.Repositories;

namespace OrderSystem.Application.Orders.Queries.GetAllOrder;

public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, List<AllOrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    
    public GetAllOrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<List<AllOrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync(cancellationToken);

        return orders.Select(s => new AllOrderDto
        {
            Id = s.Id.Value,
            CustomerId = s.CustomerId.Value,
            OrderStatus = s.Status.Value
        }).ToList();
    }
}