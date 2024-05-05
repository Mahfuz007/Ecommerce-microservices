using MediatR;

namespace Ordering.Domain.Abstracts;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    DateTime EvenetOccuredOn => DateTime.UtcNow;
    string EventType => GetType().AssemblyQualifiedName;
}
