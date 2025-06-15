using MediatR;
using OrderSystem.Domain.Customers.Repositories;
using OrderSystem.Domain.Customers.ValueObjects;

namespace OrderSystem.Application.Customers.Queries.GetCustomerByEmail;

public class GetCustomerByEmailQueryHandler : IRequestHandler<GetCustomerByEmailQuery, CustomerByEmailDto>
{
        
    private readonly ICustomerRepository _customerRepository;

    
    public GetCustomerByEmailQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    
    public async Task<CustomerByEmailDto> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        var customerEmail = new Email(request.Email);
        var customer = await _customerRepository.GetByEmailAsync(customerEmail, cancellationToken);

        if (customer is null)
            throw new InvalidOperationException($"Покупатель с Email={request.Email} не найден.");

        return new CustomerByEmailDto()
        {
            Id = customer.Id.Value,
            Email = customer.Email.Value,
            FullName = customer.FullName
        };
    }

    
}