using MediatR;

namespace OrderSystem.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;