namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract
{
    public interface ITaskHttpLockStore
    {
        SemaphoreSlim GetOrCreate(string id);
    }
}
