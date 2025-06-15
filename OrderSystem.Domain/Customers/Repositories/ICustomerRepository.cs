using OrderSystem.Domain.Customers.Entities;
using OrderSystem.Domain.Customers.ValueObjects;

namespace OrderSystem.Domain.Customers.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(CustomerId id, CancellationToken cancellationToken = default);

    Task<Customer?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);

    Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);

    Task DeleteAsync(CustomerId id, CancellationToken cancellationToken = default);
}