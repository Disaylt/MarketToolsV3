using System.Net;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.Cookies.Services.Abstract
{
    public interface ICookieContainerService
    {
        Task<CookieContainer> CreateByTask(string id);
        Task RefreshByTask(string id, CookieContainer container);
    }
}
