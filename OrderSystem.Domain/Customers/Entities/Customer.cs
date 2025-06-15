using OrderSystem.Domain.Common.Entities;
using OrderSystem.Domain.Customers.ValueObjects;

namespace OrderSystem.Domain.Customers.Entities;

public sealed class Customer : Entity<CustomerId>
{
    public string FullName { get; private set; }

    public Email Email { get; private set; }

    private Customer() : base(default!) { } 

    public Customer(CustomerId id, string fullName, Email email) : base(id)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Имя не может быть пустым", nameof(fullName));

        FullName = fullName.Trim();
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public void Rename(string newFullName)
    {
        if (string.IsNullOrWhiteSpace(newFullName))
            throw new ArgumentException("Имя не может быть пустым", nameof(newFullName));

        FullName = newFullName.Trim();
    }

    public void ChangeEmail(Email newEmail)
    {
        Email = newEmail ?? throw new ArgumentNullException(nameof(newEmail));
    }
}