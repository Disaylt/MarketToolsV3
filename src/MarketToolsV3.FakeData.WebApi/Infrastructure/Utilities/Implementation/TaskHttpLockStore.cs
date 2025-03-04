using System.Collections.Concurrent;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Implementation
{
    public class TaskHttpLockStore : ITaskHttpLockStore
    {
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _idAndSemaphoreSlimPair = new();
        public SemaphoreSlim GetOrCreate(string id)
        {
             return _idAndSemaphoreSlimPair.GetOrAdd(id, new SemaphoreSlim(1));
        }
    }
}
