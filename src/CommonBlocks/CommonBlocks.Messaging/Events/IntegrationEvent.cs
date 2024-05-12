namespace CommonBlocks.Messaging.Events;

public record IntegrationEvent
{
    Guid EventId => Guid.NewGuid();
    DateTime EvenetOccuredOn => DateTime.UtcNow;
    string EventType => GetType().AssemblyQualifiedName!;
}
