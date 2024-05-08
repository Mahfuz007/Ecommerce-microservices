namespace Ordering.Application.Dtos;

public record AddressDto(
        string firstName, 
        string lastName, 
        string email, 
        string addressLine, 
        string city, 
        string state, 
        string zipCode, 
        string country
    );
