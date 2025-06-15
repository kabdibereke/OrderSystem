using MediatR;
using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Domain.Orders.Repositories;
using OrderSystem.Domain.Orders.ValueObjects;

namespace OrderSystem.Application.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CancelOrderCommandHandler(IOrderRepository orderRepository,  IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.FromGuid(request.Id);
       

        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

        if (order is null)
        {
            throw new InvalidOperationException($"Заказ с Id={request.Id} не найден.");
        }
        
        order.Cancel();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}