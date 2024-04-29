namespace Basket.API.Entities;

public class ShoppingCartItem
{
    public string ProductName { get; set; } = default!;
    public string ProductId { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public int Color { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}
