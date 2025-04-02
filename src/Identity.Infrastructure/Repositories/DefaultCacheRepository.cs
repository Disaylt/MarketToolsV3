using Identity.Domain.Seed;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Threading;

namespace Identity.Infrastructure.Repositories
{
    internal class DefaultCacheRepository(IDistributedCache distributedCache)
        : ICacheRepository
    {
        public async Task DeleteAsync<T>(string key, CancellationToken cancellationToken) where T : class
        {
            string typeKey = BuildKey<T>(key);

            await distributedCache.RemoveAsync(typeKey, cancellationToken);
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToke) where T : class
        {
            string typeKey = BuildKey<T>(key);
            string? value = await distributedCache.GetStringAsync(typeKey, cancellationToke);

            if (value == null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expire, CancellationToken cancellationToke) where T : class
        {
            string strValue = JsonSerializer.Serialize(value);
            string typeKey = BuildKey<T>(key);

            await distributedCache.SetStringAsync(typeKey, strValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expire,
            }, cancellationToke);

        }

        private static string BuildKey<TKey>(string key)
        {
            return $"{typeof(TKey).FullName}-{key}";
        }
    }
}
