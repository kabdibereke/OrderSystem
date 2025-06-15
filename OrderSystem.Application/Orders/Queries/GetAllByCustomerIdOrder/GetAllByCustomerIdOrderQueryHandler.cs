using MediatR;
using OrderSystem.Domain.Customers.ValueObjects;
using OrderSystem.Domain.Orders.Repositories;

namespace OrderSystem.Application.Orders.Queries.GetAllByCustomerIdOrder;

public class GetAllByCustomerIdOrderQueryHandler: IRequestHandler<GetAllByCustomerIdOrderQuery, List<OrderByCustomerIdDto>>
{
    private readonly IOrderRepository _orderRepository;
    
    public GetAllByCustomerIdOrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async  Task<List<OrderByCustomerIdDto>> Handle(GetAllByCustomerIdOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(request.CustomerId,cancellationToken);

        return orders.Select(s => new OrderByCustomerIdDto
        {
            Id = s.Id.Value,
            CustomerId = s.CustomerId.Value,
            OrderStatus = s.Status.Value
        }).ToList();
    }
}