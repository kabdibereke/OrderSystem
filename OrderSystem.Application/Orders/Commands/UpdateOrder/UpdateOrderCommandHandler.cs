using MediatR;
using OrderSystem.Domain.Catalog.ValueObjects;
using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Domain.Orders.Entities;
using OrderSystem.Domain.Orders.Repositories;
using OrderSystem.Domain.Orders.ValueObjects;
using OrderSystem.Domain.Products.Repositories;

namespace OrderSystem.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Guid>
{
    
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.FromGuid(request.OrderId);
       

        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);

        if (order is null)
        {
            throw new InvalidOperationException($"Заказ с Id={request.OrderId} не найден.");
        }

        var newOrder = new Order(orderId, order.CustomerId);

        foreach (var orderItemDto in request.Items)
        {
            var productId = ProductId.FromGuid(orderItemDto.ProductId);
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

            if (product is null)
                throw new InvalidOperationException($"Продукт с ID {productId} не найден");

            if (orderItemDto.Quantity <= 0)
                throw new InvalidOperationException($"Количество должно быть больше 0. ProductId: {productId}");
            
            var orderItem = new OrderItem(
                product.Id,
                product.Name.Value,
                orderItemDto.Quantity,
                product.Price.Value
            );

            newOrder.AddItem(orderItem);
        }
        
        await _orderRepository.UpdateAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id.Value;
        
    }
}