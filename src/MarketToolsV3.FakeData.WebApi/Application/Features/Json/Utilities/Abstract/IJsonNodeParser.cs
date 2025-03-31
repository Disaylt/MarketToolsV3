using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.Json.Utilities.Abstract
{
    public interface IJsonNodeParser
    {
        JsonNode? ParseByPath(JsonNode node, params string[] pathName);
    }
}
