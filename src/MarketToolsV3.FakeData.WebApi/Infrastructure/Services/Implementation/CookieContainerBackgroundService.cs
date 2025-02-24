using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Abstract;
using System.Net;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation
{
    public class CookieContainerBackgroundService()
        : ICookieContainerBackgroundService
    {
        public Task<CookieContainer> CreateByTask(string id)
        {
            throw new NotImplementedException();
        }

        public Task RefreshByTask(string id, CookieContainer container)
        {
            throw new NotImplementedException();
        }
    }
}
