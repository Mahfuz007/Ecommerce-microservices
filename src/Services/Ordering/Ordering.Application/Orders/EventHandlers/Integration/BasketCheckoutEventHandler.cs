using CommonBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender _sender, ILogger<BasketCheckoutEventHandler> _logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        _logger.LogInformation($"Integation EventHandler : {context.Message.GetType().Name}");
        var orderCommand = MapToOrderCommand(context.Message);

        await _sender.Send(orderCommand);
    }

    private CreateOrderCommand MapToOrderCommand(BasketCheckoutEvent message)
    {
        var shippingAddress = new AddressDto(message.FirstName, message.LastName, message.Email, message.AddressLine, message.City, message.State, message.ZipCode, message.Country);
        var billingAddress = new AddressDto(message.FirstName, message.LastName, message.Email, message.AddressLine, message.City, message.State, message.ZipCode, message.Country);
        var paymentDetails = new PaymentDto(message.Name, message.CardNumber, message.CVV, message.ExpiryDate, message.PaymentMethod);
        var orderId = Guid.NewGuid();
        var orderDto = new OrderDto(
                orderId: orderId,
                name: message.UserName,
                customerId: message.CustomerId,
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                paymentDetails: paymentDetails,
                status: Domain.Enums.OrderStatus.Pending,
                orderItems: [
                    new OrderItemDto(orderId, new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf1"), 2, 150000),
                    new OrderItemDto(orderId, new Guid("686e5572-7d9b-4c0f-b7d4-c9452eff7cf3"), 1, 140000)
                ]

            );

        return new CreateOrderCommand(orderDto);
    }
}
