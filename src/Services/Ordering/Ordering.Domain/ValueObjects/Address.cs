namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string AddressLine { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string Country { get; set; } = default!;

    protected Address() { }

    private Address(string firstName, string lastName, string? email, string addressLine, string city, string state, string zipCode, string country)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AddressLine = addressLine;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }

    public static Address Of(string firstName, string lastName, string? email, string addressLine, string city, string state, string zipCode, string country)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

        return new Address(firstName, lastName, email, addressLine, city, state, zipCode, country);
    }
}
