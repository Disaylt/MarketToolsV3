namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract
{
    public interface ITaskHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage  request);
    }
}
