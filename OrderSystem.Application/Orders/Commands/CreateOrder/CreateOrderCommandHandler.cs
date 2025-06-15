using MediatR;
using OrderSystem.Domain.Catalog.ValueObjects;
using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Domain.Customers.Repositories;
using OrderSystem.Domain.Customers.ValueObjects;
using OrderSystem.Domain.Orders.Entities;
using OrderSystem.Domain.Orders.Repositories;
using OrderSystem.Domain.Orders.ValueObjects;
using OrderSystem.Domain.Products.Repositories;

namespace OrderSystem.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.New();
        var customerId = CustomerId.FromGuid(request.CustomerId);
        var order = new Order(orderId, customerId);

        foreach (var itemDto in request.Items)
        {
            var productId = ProductId.FromGuid(itemDto.ProductId);
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

            if (product is null)
                throw new InvalidOperationException($"Продукт с ID {productId} не найден");

            if (itemDto.Quantity <= 0)
                throw new InvalidOperationException($"Количество должно быть больше 0. ProductId: {productId}");

            var orderItem = new OrderItem(
                product.Id,
                product.Name.Value,
                itemDto.Quantity,
                product.Price.Value
            );

            order.AddItem(orderItem);
        }

        await _orderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id.Value;
    }
}