using MediatR;

namespace OrderSystem.Application.Orders.Queries.GetAllByCustomerIdOrder;

public record GetAllByCustomerIdOrderQuery(Guid CustomerId) : IRequest<List<OrderByCustomerIdDto>>;
