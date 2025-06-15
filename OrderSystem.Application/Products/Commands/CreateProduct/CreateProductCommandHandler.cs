using MediatR;
using OrderSystem.Domain.Catalog.ValueObjects;
using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Domain.Products.Entities;
using OrderSystem.Domain.Products.Repositories;
using OrderSystem.Infrastructure.Persistence.DbContexts;

namespace OrderSystem.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, Guid>
{
    
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async  Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var name = new ProductName(request.Name);
        var price = new Price(request.Price);

        var product = new Product(ProductId.New(), name, price);

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product.Id.Value;
    }
}