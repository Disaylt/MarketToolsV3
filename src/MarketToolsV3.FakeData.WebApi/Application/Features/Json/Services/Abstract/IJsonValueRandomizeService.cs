namespace MarketToolsV3.FakeData.WebApi.Application.Features.Json.Services.Abstract
{
    public interface IJsonValueRandomizeService<out T>
    {
        T Create(int min, int max);
    }
}
