namespace Ordering.Application.Orders.EventHandlers;

public class OrderUpdateEventHandler(ILogger<OrderUpdateEventHandler> _logger) : INotificationHandler<OrderUpdateEvent>
{
    public Task Handle(OrderUpdateEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Domain Event Handled: {domainEvent.GetType().Name}");
        return Task.CompletedTask;
    }
}
