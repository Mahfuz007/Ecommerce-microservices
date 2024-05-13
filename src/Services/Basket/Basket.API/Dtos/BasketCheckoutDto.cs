namespace Basket.API.Dtos;

public class BasketCheckoutDto
{
    public string UserName { get; set; } = default!;
    public Guid CustomerId { get; set; }
    public decimal TotalPrice { get; set; }

    //Shipping and Billing Addess

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Email { get; set; }
    public string AddressLine { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string Country { get; set; } = default!;

    //Payment information
    public string Name { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public string ExpiryDate { get; set; } = default!;
    public int PaymentMethod { get; set; }
}