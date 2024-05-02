using Discount.Grpc;

namespace Basket.API.Features.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart.UserName).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Cart.Items).NotEmpty();
    }
}

public class StoreBasketCommandHandler(IBasketRepository _repository, DiscountProtoService.DiscountProtoServiceClient _discountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = new ShoppingCart()
        {
            UserName = command.Cart.UserName,
            Items = command.Cart.Items
        };
        await DeductDiscount(basket);
        var result = await _repository.StoreBasket(basket, cancellationToken);
        return new StoreBasketResult(result);
    }

    private async Task DeductDiscount(ShoppingCart cart)
    {
        foreach(var item in cart.Items)
        {
            var coupon = await _discountClient.GetDiscountAsync(new GetDiscountRequest { Name = item.ProductName });
            item.Price -= coupon.Amount;
        }
    }
}