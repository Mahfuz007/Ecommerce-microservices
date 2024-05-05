namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }
    private ProductId(Guid value)  => Value = value;

    public static ProductId Of(Guid Id)
    {
        ArgumentNullException.ThrowIfNull(Id);
        return new ProductId(Id);
    }
}
