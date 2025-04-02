using System.Net;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.Mappers
{
    public class CookieFromCookieEntityMapper : IFromMapper<Cookie, CookieEntity>
    {
        public CookieEntity Map(Cookie value)
        {
            return new()
            {
                Domain = value.Domain,
                Name = value.Name,
                Value = value.Value,
                Path = value.Path
            };
        }
    }
}
