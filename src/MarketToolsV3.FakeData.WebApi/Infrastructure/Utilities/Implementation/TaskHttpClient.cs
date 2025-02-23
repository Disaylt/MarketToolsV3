using MarketToolsV3.FakeData.WebApi.Infrastructure.Models;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Implementation
{
    public class TaskHttpClient(HttpClientHandlerInfoModel httpHandlerInfo, 
        ICookieContainerBackgroundService cookieContainerBackgroundService)
        : HttpClient(httpHandlerInfo.Handler), ITaskHttpClient
    {
        public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            await cookieContainerBackgroundService.RefreshByTask(
                httpHandlerInfo.Id,
                httpHandlerInfo.Handler.CookieContainer);

            return response;
        }
    }
}
