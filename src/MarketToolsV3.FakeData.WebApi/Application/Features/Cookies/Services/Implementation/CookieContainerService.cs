using MarketToolsV3.FakeData.WebApi.Application.Features.Cookies.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using System.Net;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.Cookies.Services.Implementation
{
    public class CookieContainerService(IMapperFactory mapperFactory,
        ICookieEntityService cookieEntityService)
        : ICookieContainerService
    {
        public async Task<CookieContainer> CreateByTask(string id)
        {
            var mapper = mapperFactory.CreateToMapper<CookieEntity, Cookie>();
            var entities = await cookieEntityService.GetRangeByTaskAsync(id);
            var cookies = entities.Select(mapper.Map);

            return CreateCookieContainer(cookies);
        }

        public async Task RefreshByTask(string id, CookieContainer container)
        {
            var mapper = mapperFactory.CreateFromMapper<Cookie, CookieEntity>();
            await cookieEntityService.ClearByTaskIdAsync(id);
            IEnumerable<CookieEntity> cookies = container.GetAllCookies()
                .Select(mapper.Map)
                .Select(x =>
                {
                    x.TaskId = id;
                    return x;
                });
            await cookieEntityService.AddRangeAsync(cookies);
        }

        private CookieContainer CreateCookieContainer(IEnumerable<Cookie> cookies)
        {
            var container = new CookieContainer(100, 100, 4096);

            foreach (var cookie in cookies)
            {
                container.Add(cookie);
            }

            return container;
        }
    }
}
