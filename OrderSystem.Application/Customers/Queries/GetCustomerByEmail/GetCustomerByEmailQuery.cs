using MediatR;

namespace OrderSystem.Application.Customers.Queries.GetCustomerByEmail;

public record GetCustomerByEmailQuery(string Email) : IRequest<CustomerByEmailDto>;
