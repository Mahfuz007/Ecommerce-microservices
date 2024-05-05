namespace Ordering.Domain.Abstracts;

public abstract class Aggregrator<T> : Entity<T>, IAggregrator<T>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvents()
    {
        IDomainEvent[] dequeueEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequeueEvents;
    }
}