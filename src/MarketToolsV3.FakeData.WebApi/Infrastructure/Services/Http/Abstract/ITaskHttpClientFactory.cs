namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Abstract
{
    public interface ITaskHttpClientFactory
    {
        Task<ITaskHttpClient> CreateAsync(string id);
    }
}
