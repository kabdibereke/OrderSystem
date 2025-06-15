using MediatR;

namespace OrderSystem.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<List<ProductDto>>;