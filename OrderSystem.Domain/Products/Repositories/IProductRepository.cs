using OrderSystem.Domain.Products.Entities;
using OrderSystem.Domain.Catalog.ValueObjects;

namespace OrderSystem.Domain.Products.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Product product, CancellationToken cancellationToken = default);

    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

    Task DeleteAsync(ProductId id, CancellationToken cancellationToken = default);
}