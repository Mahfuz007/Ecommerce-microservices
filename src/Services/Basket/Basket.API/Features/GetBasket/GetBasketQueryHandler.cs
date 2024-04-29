using Basket.API.Exceptions;

namespace Basket.API.Features.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await _repository.GetBasket(query.UserName, cancellationToken);
        if (basket == null)
        {
            throw new BasketNotFoundException(query.UserName);
        }
        return new GetBasketResult(basket);
    }
}