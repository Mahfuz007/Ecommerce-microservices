using Ordering.Domain.Abstracts;

namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }

    private Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public static Product Create(string name, decimal price)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(price, 0);

        return new Product(name, price);
    }

}