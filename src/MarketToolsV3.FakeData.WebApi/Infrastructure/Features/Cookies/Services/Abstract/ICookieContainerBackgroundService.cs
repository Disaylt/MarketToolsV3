using System.Net;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Features.Cookies.Services.Abstract
{
    public interface ICookieContainerBackgroundService
    {
        Task<CookieContainer> CreateByTask(string id);
        Task RefreshByTask(string id, CookieContainer container);
    }
}
