using MediatR;

namespace OrderSystem.Application.Orders.Commands.CancelOrder;

public record CancelOrderCommand(Guid Id) : IRequest;
