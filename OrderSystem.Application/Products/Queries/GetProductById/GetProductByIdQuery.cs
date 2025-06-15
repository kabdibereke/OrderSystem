using MediatR;
using OrderSystem.Application.Products.Queries.GetAllProducts;

namespace OrderSystem.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id): IRequest<ProductByIdDto>;
