namespace Ordering.Application.Dtos;

public record PaymentDto(
        string name, 
        string cardNumber, 
        string Cvv, 
        string expiryDate, 
        int paymentMethod
    );