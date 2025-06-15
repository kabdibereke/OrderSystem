using MediatR;
using OrderSystem.Domain.Catalog.ValueObjects;
using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Domain.Products.Repositories;

namespace OrderSystem.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productId = ProductId.FromGuid(request.Id);

        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (product is null)
            throw new InvalidOperationException($"Продукт с Id = {request.Id} не найден.");

        await _productRepository.DeleteAsync(productId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}