namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string Name { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public string ExpiryDate { get; set; } = default!;
    public int PaymentMethod { get; set; }
    protected Payment()
    {

    }

    public Payment(string name, string cardNumber, string Cvv, string expiryDate, int paymentMethod)
    {
        Name = name;
        CardNumber = cardNumber;
        CVV = Cvv;
        ExpiryDate = expiryDate;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string name, string cardNumber, string Cvv, string expiryDate, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(Cvv);
        ArgumentException.ThrowIfNullOrWhiteSpace(expiryDate);
        ArgumentOutOfRangeException.ThrowIfNotEqual(Cvv.Length, 3);

        return new Payment(name, cardNumber, Cvv, expiryDate, paymentMethod);
    }
}
