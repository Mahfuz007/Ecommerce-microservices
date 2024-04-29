

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public class BasketCacheRepository(IBasketRepository _repository, IDistributedCache _cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var cacheBasket = await _cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cacheBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
            }

            var basket = await _repository.GetBasket(userName, cancellationToken);
            await _cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize<ShoppingCart>(basket));
            return basket;
        }

        public async Task<string> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken)
        {
            string userName = await _repository.StoreBasket(cart, cancellationToken);
            await _cache.SetStringAsync(userName, JsonSerializer.Serialize<ShoppingCart>(cart));
            return userName;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteBasket(userName,cancellationToken);
            await _cache.RemoveAsync(userName, cancellationToken);
            return result;
        }
    }
}
