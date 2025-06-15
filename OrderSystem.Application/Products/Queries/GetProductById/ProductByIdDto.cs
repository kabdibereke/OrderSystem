namespace OrderSystem.Application.Products.Queries.GetProductById;

public class ProductByIdDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public decimal Price { get; init; }
}