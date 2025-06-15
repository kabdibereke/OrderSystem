using MediatR;

namespace OrderSystem.Application.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerByIdDto>;
