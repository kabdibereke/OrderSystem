using MediatR;
using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Domain.Customers.Entities;
using OrderSystem.Domain.Customers.Repositories;
using OrderSystem.Domain.Customers.ValueObjects;
using OrderSystem.Domain.Products.Repositories;

namespace OrderSystem.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler: IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);

        var customer = new Customer(CustomerId.New(), request.FullName, email);

        await _customerRepository.AddAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return customer.Id.Value;
    }
}