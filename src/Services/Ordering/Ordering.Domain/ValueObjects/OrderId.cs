namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; }
    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid orderId)
    {
        ArgumentNullException.ThrowIfNull(orderId);
        if (orderId == Guid.Empty)
        {
            throw new DomainException("OrderId cannot be empty.");
        }
        return new OrderId(orderId);
    }
}