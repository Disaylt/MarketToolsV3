namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract
{
    public interface ITaskHttpClientFactory
    {
        ITaskHttpClient CreateHttpClient(int id);
    }
}
