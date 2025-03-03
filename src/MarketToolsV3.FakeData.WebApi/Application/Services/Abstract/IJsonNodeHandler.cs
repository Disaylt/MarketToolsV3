using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface IJsonNodeHandler
    {
        Task HandleAsync(string taskId, JsonNode node);
    }
}
