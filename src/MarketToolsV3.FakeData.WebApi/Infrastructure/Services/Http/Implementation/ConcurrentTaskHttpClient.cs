using MarketToolsV3.FakeData.WebApi.Infrastructure.Models;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Implementation
{
    public class ConcurrentTaskHttpClient(ITaskHttpClient taskHttpClient,
        HttpClientHandlerInfoModel httpHandlerInfo,
        ITaskHttpLockStore taskHttpLockStore)
        : ITaskHttpClient
    {
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            SemaphoreSlim semaphoreSlim = taskHttpLockStore.GetOrCreate(httpHandlerInfo.Id);

            try
            {
                await semaphoreSlim.WaitAsync(cancellationToken);

                return await taskHttpClient.SendAsync(request, cancellationToken);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
