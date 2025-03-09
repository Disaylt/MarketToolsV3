namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Abstract
{
    public interface ITaskHttpLockStore
    {
        SemaphoreSlim GetOrCreate(string id);
    }
}
