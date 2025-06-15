using MediatR;

namespace OrderSystem.Application.Orders.Commands.ConfirmOrder;

public record ConfirmOrderCommand(Guid Id) : IRequest;
