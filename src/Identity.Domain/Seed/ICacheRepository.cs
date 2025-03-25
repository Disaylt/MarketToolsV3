using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public interface ICacheRepository
    {
        Task<T?> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string key, T value, TimeSpan expire) where T : class;
        Task DeleteAsync<T>(string key, CancellationToken cancellationToken) where T : class;
    }
}
