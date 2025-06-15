using OrderSystem.Domain.Catalog.ValueObjects;
using OrderSystem.Domain.Common.Entities;

namespace OrderSystem.Domain.Products.Entities;

public sealed class Product : Entity<ProductId>
{
    public ProductName Name { get; private set; }

    public Price Price { get; private set; }
    
    private Product() : base(default!) { }


    public Product(ProductId id, ProductName name, Price price) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price ?? throw new ArgumentNullException(nameof(price));
    }

    public void Rename(ProductName newName)
    {
        Name = newName ?? throw new ArgumentNullException(nameof(newName));
    }

    public void ChangePrice(Price newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    }
}