using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public interface ICacheRepository<T> where T : class
    {
        Task<T?> GetAsync(string key);
        Task SetAsync(string key, T value, TimeSpan expire);
        Task DeleteAsync(string key, CancellationToken cancellationToken);
    }
}
