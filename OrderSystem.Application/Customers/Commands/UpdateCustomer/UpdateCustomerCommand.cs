using MediatR;

namespace OrderSystem.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand: IRequest<Guid>
{
    public Guid Id { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; }= default!;
}