using MediatR;

namespace OrderSystem.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}