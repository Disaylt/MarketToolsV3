using System.Net;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using MarketToolsV3.FakeData.WebApi.Application.Features.Cookies.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.Cookies.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Features.Cookies.Services.Implementation
{
    public class CookieContainerBackgroundService(IServiceProvider serviceProvider)
        : ICookieContainerBackgroundService
    {
        public async Task<CookieContainer> CreateByTask(string id)
        {
            using var scope = serviceProvider.CreateScope();

            return await scope.ServiceProvider
                .GetRequiredService<ICookieContainerService>()
                .CreateByTask(id);
        }

        public async Task RefreshByTask(string id, CookieContainer container)
        {
            using var scope = serviceProvider.CreateScope();

            await scope.ServiceProvider
                .GetRequiredService<ICookieContainerService>()
                .RefreshByTask(id, container);

            await scope.ServiceProvider
                .GetRequiredService<IUnitOfWork>()
                .SaveChangesAsync();
        }
    }
}
