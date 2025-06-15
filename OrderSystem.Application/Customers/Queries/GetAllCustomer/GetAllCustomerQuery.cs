using MediatR;

namespace OrderSystem.Application.Customers.Queries.GetAllCustomer;

public record GetAllCustomerQuery() : IRequest<List<CustomerDto>>;
