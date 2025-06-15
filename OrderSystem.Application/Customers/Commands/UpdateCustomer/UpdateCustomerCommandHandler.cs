using MediatR;
using OrderSystem.Application.Customers.Commands.CreateCustomer;
using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Domain.Customers.Repositories;
using OrderSystem.Domain.Customers.ValueObjects;

namespace OrderSystem.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler: IRequestHandler<UpdateCustomerCommand, Guid>
{
    
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerId = CustomerId.FromGuid(request.Id);
        
        var customer = await _customerRepository.GetByIdAsync(customerId, cancellationToken);

        
        if (customer is null)
            throw new InvalidOperationException($"Покупатель с Id={request.Id} не найден.");
        
        customer.Rename(request.FullName);
        customer.ChangeEmail(new Email(request.Email));
        
        await _customerRepository.UpdateAsync(customer, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return customer.Id.Value;
    }
}