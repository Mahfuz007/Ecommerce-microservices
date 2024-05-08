namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IApplicationDbContext _dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.FindAsync([OrderId.Of(command.Order.orderId)], cancellationToken: cancellationToken);
        if(order == null)
        {
            throw new OrderNotFoundException(command.Order.orderId.ToString());
        }

        UpdateOrderWithNewValue(order, command.Order);
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    private void UpdateOrderWithNewValue(Order order, OrderDto orderDto)
    {
        var updatedShippingAddress = Address.Of(orderDto.shippingAddress.firstName, orderDto.shippingAddress.lastName, orderDto.shippingAddress.email, orderDto.shippingAddress.addressLine, orderDto.shippingAddress.city, orderDto.shippingAddress.state, orderDto.shippingAddress.zipCode, orderDto.shippingAddress.country);
        var updatedBillingAddress = Address.Of(orderDto.billingAddress.firstName, orderDto.billingAddress.lastName, orderDto.billingAddress.email, orderDto.billingAddress.addressLine, orderDto.billingAddress.city, orderDto.billingAddress.state, orderDto.billingAddress.zipCode, orderDto.billingAddress.country);
        var updatePaymentDetails = Payment.Of(orderDto.paymentDetails.name, orderDto.paymentDetails.cardNumber, orderDto.paymentDetails.Cvv, orderDto.paymentDetails.expiryDate, orderDto.paymentDetails.paymentMethod);

        order.Update(
                name: OrderName.Of(orderDto.name),
                shippingAddress: updatedShippingAddress,
                billingAddress: updatedBillingAddress,
                paymentDetails: updatePaymentDetails,
                status: orderDto.status
            );
    }
}