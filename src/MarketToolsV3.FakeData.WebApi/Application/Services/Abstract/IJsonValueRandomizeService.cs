using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface IJsonValueRandomizeService
    {
        ICollection<JsonValue> FindRandomTemplateValues(JsonNode? node);
        void GenerateRandomValue(JsonValue  value);
    }
}