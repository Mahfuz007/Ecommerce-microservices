namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand (OrderDto Order) : ICommand<UpdateOrderResult>
{
}

public record UpdateOrderResult(bool IsSuccess) { }

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.Order).NotNull();
        RuleFor(x => x.Order.name).NotEmpty();
        RuleFor(x => x.Order.customerId).NotEmpty();
        RuleFor(x => x.Order.orderId).NotEmpty();
        RuleFor(x => x.Order.shippingAddress).NotEmpty();
        RuleFor(x => x.Order.billingAddress).NotEmpty();
        RuleFor(x => x.Order.orderItems).NotEmpty();
        RuleFor(x => x.Order.paymentDetails).NotEmpty();
    }
}
