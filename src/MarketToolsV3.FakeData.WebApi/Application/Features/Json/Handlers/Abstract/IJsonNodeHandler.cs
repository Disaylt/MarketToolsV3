using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.Json.Handlers.Abstract
{
    public interface IJsonNodeHandler
    {
        Task HandleAsync(string taskId, JsonNode node);
    }
}
