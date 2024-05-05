using Ordering.Domain.Abstracts;

namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public static Customer Create(string Name, String Email)
    {
        ArgumentException.ThrowIfNullOrEmpty(Name);
        ArgumentException.ThrowIfNullOrEmpty(Email);

        return new Customer(Name, Email);
    }
}
