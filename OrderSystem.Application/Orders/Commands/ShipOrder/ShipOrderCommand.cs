using MediatR;

namespace OrderSystem.Application.Orders.Commands.ShipOrder;

public record ShipOrderCommand(Guid Id) : IRequest;
