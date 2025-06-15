using OrderSystem.Domain.Orders.Entities;
using OrderSystem.Domain.Orders.ValueObjects;

namespace OrderSystem.Domain.Orders.Repositories;

public interface IOrderRepository
{
    
    Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);

    Task AddAsync(Order order, CancellationToken cancellationToken = default);

    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
}