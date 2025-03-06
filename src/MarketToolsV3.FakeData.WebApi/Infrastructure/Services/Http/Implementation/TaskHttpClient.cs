using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.Cookies.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Models;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Implementation
{
    public class TaskHttpClient(HttpClientHandlerInfoModel httpHandlerInfo,
        ICookieContainerBackgroundService cookieContainerBackgroundService,
        ILogger<TaskHttpClient> logger)
        : HttpClient(httpHandlerInfo.Handler), ITaskHttpClient
    {
        public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            logger.LogInformation("Http response. Status code: {status}. Response body: {response}", response.StatusCode, responseContent);

            await cookieContainerBackgroundService.RefreshByTask(
                httpHandlerInfo.Id,
                httpHandlerInfo.Handler.CookieContainer);

            return response;
        }
    }
}
