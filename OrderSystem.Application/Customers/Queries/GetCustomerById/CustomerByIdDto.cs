namespace OrderSystem.Application.Customers.Queries.GetCustomerById;

public class CustomerByIdDto
{
    public Guid Id { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; }= default!;
}