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

public class StoreBasketCommandHandler(IBasketRepository _repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = new ShoppingCart()
        {
            UserName = command.Cart.UserName,
            Items = command.Cart.Items
        };
        var result = await _repository.StoreBasket(basket, cancellationToken);
        return new StoreBasketResult(result);
    }
}