using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCacheService;
        private readonly ISerializeService _serializeService;
        private readonly ILogger _logger;

        public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, ILogger logger)
        {
            _redisCacheService = redisCacheService;
            _serializeService = serializeService;
            _logger = logger;
        }
        public async Task<bool> DeleteBasketFromUserName(string userName)
        {
            //var result = _redisCacheService.RemoveAsync(userName)
            try
            {
                _logger.Information("BEGIN: DeleteBasketFromUserName", userName);
                await _redisCacheService.RemoveAsync(userName);
                _logger.Information("END: DeleteBasketFromUserName", userName);
                return true;
            } 
            catch(Exception ex)
            {
                _logger.Error("DeleteBasketFromUserName: ", ex.Message);
                throw;
            }
        }

        public async Task<Cart?> GetBasketByUserName(string userName)
        {
            _logger.Information("BEGIN: GetBasketByUserName", userName);
            var basket = await _redisCacheService.GetStringAsync(userName);
            _logger.Information("END: GetBasketByUserName", userName);
            return string.IsNullOrEmpty(basket) ? null : _serializeService.Deserialize<Cart>(basket);
        }

        public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {
            if(options != null)
            {
                _logger.Information("BEGIN: UpdateBasket with options", cart.UserName);
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart), options);
                _logger.Information("END: UpdateBasket with options", cart.UserName);
            }
            else
            {
                _logger.Information("BEGIN: UpdateBasket without options", cart.UserName);
                await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart));
                _logger.Information("END: UpdateBasket without options", cart.UserName);
            }

            return await GetBasketByUserName(cart.UserName);
        }
    }
}
