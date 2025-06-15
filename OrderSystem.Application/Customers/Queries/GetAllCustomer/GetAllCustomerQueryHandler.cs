using MediatR;
using OrderSystem.Domain.Customers.Repositories;

namespace OrderSystem.Application.Customers.Queries.GetAllCustomer;

public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, List<CustomerDto>>
{
    
    private readonly ICustomerRepository _customerRepository;

    
    public GetAllCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<List<CustomerDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);

        return customers.Select(c => new CustomerDto
        {
            Id = c.Id.Value,
            FullName = c.FullName,
            Email = c.Email.Value
        }).ToList();
    }
}