using MediatR;
using OrderSystem.Application.Products.Commands.CreateProduct;
using OrderSystem.Domain.Catalog.ValueObjects;
using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Domain.Products.Repositories;

namespace OrderSystem.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler: IRequestHandler<UpdateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productId = ProductId.FromGuid(request.Id);
        
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        
        if (product is null)
            throw new InvalidOperationException($"Продукт с Id={request.Id} не найден.");
        
        product.Rename(new ProductName(request.Name));
        product.ChangePrice(new Price(request.Price));
        
        await _productRepository.UpdateAsync(product, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id.Value;
    }
}