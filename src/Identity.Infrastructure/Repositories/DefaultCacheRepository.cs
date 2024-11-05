using Identity.Domain.Seed;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories
{
    internal class DefaultCacheRepository<T>(IDistributedCache distributedCache)
        : ICacheRepository<T> where T : class
    {
        public async Task DeleteAsync(string key, CancellationToken cancellationToken)
        {
            await distributedCache.RemoveAsync(key, cancellationToken);
        }

        public async Task<T?> GetAsync(string key)
        {
            string? value = await distributedCache.GetStringAsync(BuildKey<T>(key));

            if (value == null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync(string key, T value, TimeSpan expire)
        {
            string strValue = JsonSerializer.Serialize(value);

            await distributedCache.SetStringAsync(BuildKey<T>(key), strValue, new DistributedCacheEntryOptions
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
