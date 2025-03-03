using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class SelectTemplateJsonNodeService : BaseTemplateJsonNodeService
    {
        public override ICollection<JsonValue> Find(JsonNode? node)
        {
            if (node is JsonObject jsonObject)
            {
                return ParseJsonObject(jsonObject);
            }
            if (node is JsonArray jsonArray)
            {
                return ParseJsonArray(jsonArray);
            }
            if (node is JsonValue jsonValue &&
                jsonValue.TryGetValue(out string? strValue) &&
                strValue.StartsWith("select"))
            {
                return [jsonValue];
            }

            return [];
        }
    }
}
