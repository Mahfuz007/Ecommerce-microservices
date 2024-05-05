namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    public string Value { get; } = default!;
    private OrderName(string  value) => Value = value;
    public static OrderName Of(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return new OrderName(name);
    }
}
