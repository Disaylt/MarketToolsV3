using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class IntJsonValueRandomizeService : IJsonValueRandomizeService<int>
    {
        public int Create(int min, int max)
        {
            return Random.Shared.Next(min, max);
        }
    }
}
