using MediatR;
using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Domain.Customers.Repositories;
using OrderSystem.Domain.Customers.ValueObjects;

namespace OrderSystem.Application.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler: IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerId = CustomerId.FromGuid(request.Id);

        var customer = await _customerRepository.GetByIdAsync(customerId, cancellationToken);
        
        if (customer is null)
            throw new InvalidOperationException($"Покупатель с Id={request.Id} не найден.");

        await _customerRepository.DeleteAsync(customerId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}