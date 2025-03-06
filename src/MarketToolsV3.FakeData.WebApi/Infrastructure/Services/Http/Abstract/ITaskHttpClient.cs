namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Abstract
{
    public interface ITaskHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    }
}
