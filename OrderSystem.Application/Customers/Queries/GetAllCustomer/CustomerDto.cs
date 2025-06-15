namespace OrderSystem.Application.Customers.Queries.GetAllCustomer;

public class CustomerDto
{
    public Guid Id { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; }= default!;
}