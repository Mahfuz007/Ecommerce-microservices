namespace Ordering.Domain.Abstracts;

public interface IAggregrator<T> : IAggregrator, IEntity<T>
{

}

public interface IAggregrator : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
}
