
using Basket.API.Exceptions;

namespace Basket.API.Repositories;

public class BasketRepository(IDocumentSession _session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var basket = await _session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        return basket!;
    }

    public async Task<string> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken)
    {
        _session.Store(cart);
        await _session.SaveChangesAsync(cancellationToken);
        return cart.UserName;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
         _session.Delete<ShoppingCart>(userName);
        await _session.SaveChangesAsync(cancellationToken);
        return true;
    }
}
