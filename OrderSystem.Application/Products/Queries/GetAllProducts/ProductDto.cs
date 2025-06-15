namespace OrderSystem.Application.Products.Queries.GetAllProducts;

public class ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public decimal Price { get; init; }
}