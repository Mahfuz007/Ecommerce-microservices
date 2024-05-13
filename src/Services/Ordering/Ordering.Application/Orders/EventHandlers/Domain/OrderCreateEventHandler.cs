namespace Ordering.Application.Orders.EventHandlers.DomainEvents;

public class OrderCreateEventHandler(ILogger<OrderCreateEventHandler> _logger) : INotificationHandler<OrderCreateEvent>
{
    public Task Handle(OrderCreateEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Domain Event Handled: {domainEvent.GetType().Name}");

        return Task.CompletedTask;
    }
}