using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface IRandomJsonValueService
    {
        void GenerateRandomValue(JsonValue  value);
        void GenerateRandomValues(IEnumerable<JsonValue> values);
    }
}