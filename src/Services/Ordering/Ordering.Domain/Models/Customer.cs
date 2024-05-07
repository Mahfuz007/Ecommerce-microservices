namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;

    public static Customer Create(CustomerId customerId,  string name, String email)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(email);

        return new Customer
        {
            Id = customerId,
            Name = name,
            Email = email
        };
    }
}
