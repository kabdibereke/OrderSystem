using MediatR;

namespace OrderSystem.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand: IRequest<Guid>
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; }= default!;
}