using MediatR;
using OrderSystem.Domain.Orders.ValueObjects;

namespace OrderSystem.Application.Orders.Queries.GetByIdOrder;

public record GetByIdOrderQuery(Guid Id) : IRequest<OrderByIdDto>;



