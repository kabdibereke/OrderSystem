using MediatR;
using OrderSystem.Domain.Customers.Repositories;
using OrderSystem.Domain.Customers.ValueObjects;

namespace OrderSystem.Application.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerByIdDto>
{
        
    private readonly ICustomerRepository _customerRepository;

    
    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    
    public async Task<CustomerByIdDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customerId = CustomerId.FromGuid(request.Id);
        var customer = await _customerRepository.GetByIdAsync(customerId, cancellationToken);

        if (customer is null)
            throw new InvalidOperationException($"Покупатель с Id={request.Id} не найден.");

        return new CustomerByIdDto()
        {
            Id = customer.Id.Value,
            Email = customer.Email.Value,
            FullName = customer.FullName
        };
    }
}