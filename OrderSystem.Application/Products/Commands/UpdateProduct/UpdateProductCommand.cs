using MediatR;

namespace OrderSystem.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand: IRequest<Guid>
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}