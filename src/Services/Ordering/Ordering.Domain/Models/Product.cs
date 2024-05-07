namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }

    public static Product Create(ProductId productId, string name, decimal price)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(price, 0);

        return new Product
        {
            Id = productId,
            Name = name,
            Price = price
        };
    }

}