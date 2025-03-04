namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract
{
    public interface ITaskHttpClientFactory
    {
        Task<ITaskHttpClient> CreateAsync(string id);
    }
}
