namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(string OrderName);

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
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