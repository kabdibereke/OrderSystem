namespace OrderSystem.Application.Customers.Queries.GetCustomerByEmail;

public class CustomerByEmailDto
{
    public Guid Id { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; }= default!;
}