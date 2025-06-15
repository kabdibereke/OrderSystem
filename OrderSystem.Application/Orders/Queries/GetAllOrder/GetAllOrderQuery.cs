using MediatR;
using OrderSystem.Domain.Orders.Entities;

namespace OrderSystem.Application.Orders.Queries.GetAllOrder;

public record GetAllOrderQuery() : IRequest<List<AllOrderDto>>;
