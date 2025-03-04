using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public abstract class BaseTemplateJsonNodeService : ITemplateJsonNodeService
    {
        public abstract ICollection<JsonValue> Find(JsonNode? node);

        protected List<JsonValue> ParseJsonArray(JsonArray jsonArray)
        {
            List<JsonValue> values = [];

            foreach (var item in jsonArray)
            {
                ICollection<JsonValue> arrayValues = Find(item);
                values.AddRange(arrayValues);
            }

            return values;
        }

        protected List<JsonValue> ParseJsonObject(JsonObject jsonObject)
        {
            List<JsonValue> values = [];

            foreach (var property in jsonObject)
            {
                ICollection<JsonValue> objectValues = Find(property.Value);
                values.AddRange(objectValues);
            }

            return values;
        }
    }
}
