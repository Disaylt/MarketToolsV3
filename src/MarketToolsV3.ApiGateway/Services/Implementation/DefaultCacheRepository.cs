using System.Text.Json;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace MarketToolsV3.ApiGateway.Services.Implementation
{
    internal class DefaultCacheRepository(IDistributedCache distributedCache)
        : ICacheRepository
    {
        public async Task DeleteAsync<T>(string key, CancellationToken cancellationToken) where T : class
        {
            string typeKey = BuildKey<T>(key);

            await distributedCache.RemoveAsync(typeKey, cancellationToken);
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            string typeKey = BuildKey<T>(key);

            string? value = await distributedCache.GetStringAsync(typeKey);

            if (value == null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expire) where T : class
        {
            string strValue = JsonSerializer.Serialize(value);
            string typeKey = BuildKey<T>(key);

            await distributedCache.SetStringAsync(typeKey, strValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expire,
            });

        }

        private static string BuildKey<TKey>(string key)
        {
            return $"{typeof(TKey).FullName}-{key}";
        }
    }
}
