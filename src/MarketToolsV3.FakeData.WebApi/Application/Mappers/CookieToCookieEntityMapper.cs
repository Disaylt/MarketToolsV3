using System.Net;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.Mappers
{
    public class CookieToCookieEntityMapper : IToMapper<CookieEntity, Cookie>
    {
        public Cookie Map(CookieEntity value)
        {
            return new Cookie(value.Name, value.Value, value.Path, value.Domain);
        }
    }
}
