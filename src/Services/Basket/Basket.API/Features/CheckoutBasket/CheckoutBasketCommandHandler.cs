using CommonBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Features.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty();
    }
}
public class CheckoutBasketCommandHandler (IBasketRepository _repository, IPublishEndpoint _publish) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await _repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
        if (basket is null) return new CheckoutBasketResult(false);

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;
        await _publish.Publish(eventMessage, cancellationToken);
        await _repository.DeleteBasket(basket.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
