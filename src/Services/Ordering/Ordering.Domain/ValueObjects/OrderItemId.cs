namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;
    public static OrderItemId Of(Guid orderItemId)
    {
        ArgumentNullException.ThrowIfNull(orderItemId);
        if (orderItemId == Guid.Empty)
        {
            throw new DomainException("OrderItemId cannot be empty.");
        }

        return new OrderItemId(orderItemId);
    }
}
