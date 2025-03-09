using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.Json.Services.Abstract
{
    public interface ITemplateJsonNodeService
    {
        ICollection<JsonValue> Find(JsonNode? node);
    }
}
