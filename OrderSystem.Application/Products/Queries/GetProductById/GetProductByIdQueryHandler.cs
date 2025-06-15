using MediatR;
using OrderSystem.Application.Products.Queries.GetAllProducts;
using OrderSystem.Domain.Catalog.ValueObjects;
using OrderSystem.Domain.Products.Repositories;

namespace OrderSystem.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler: IRequestHandler<GetProductByIdQuery, ProductByIdDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductByIdDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productId = ProductId.FromGuid(request.Id);
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (product is null)
            throw new InvalidOperationException($"Продукт с Id={request.Id} не найден.");

        return new ProductByIdDto
        {
            Id = product.Id.Value,
            Name = product.Name.Value,
            Price = product.Price.Value
        };
    }
}