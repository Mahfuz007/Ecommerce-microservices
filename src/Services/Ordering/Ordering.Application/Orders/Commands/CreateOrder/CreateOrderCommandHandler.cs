namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IApplicationDbContext _dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateOrder(command.Order);
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(command.Order.name);
    }

    private Order CreateOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.Of(orderDto.shippingAddress.firstName, orderDto.shippingAddress.lastName, orderDto.shippingAddress.email, orderDto.shippingAddress.addressLine, orderDto.shippingAddress.city, orderDto.shippingAddress.state, orderDto.shippingAddress.zipCode, orderDto.shippingAddress.country);
        var billingAddress = Address.Of(orderDto.billingAddress.firstName, orderDto.billingAddress.lastName, orderDto.billingAddress.email, orderDto.billingAddress.addressLine, orderDto.billingAddress.city, orderDto.billingAddress.state, orderDto.billingAddress.zipCode, orderDto.billingAddress.country);
        var paymentDetails = Payment.Of(orderDto.paymentDetails.name, orderDto.paymentDetails.cardNumber, orderDto.paymentDetails.Cvv, orderDto.paymentDetails.expiryDate, orderDto.paymentDetails.paymentMethod);
        var order = Order.Create(
                orderId : OrderId.Of(orderDto.orderId),
                name: OrderName.Of(orderDto.name),
                customerId :CustomerId.Of(orderDto.customerId),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                paymentDetails: paymentDetails
            );

        foreach(var orderItemDto in orderDto.orderItems)
        {
            order.Add(ProductId.Of(orderItemDto.productId), orderItemDto.quantity, orderItemDto.price);
        }

        return order;
    }
}
