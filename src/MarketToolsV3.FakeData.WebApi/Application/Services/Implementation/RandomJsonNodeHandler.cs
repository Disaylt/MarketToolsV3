using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using System.Text.Json.Nodes;
using MarketToolsV3.FakeData.WebApi.Application.Enums;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class RandomJsonNodeHandler(
        IRandomJsonValueService randomJsonValueService,
        [FromKeyedServices(TemplateJsonNode.Random)]
        ITemplateJsonNodeService templateJsonNodeService)
        : IJsonNodeHandler
    {
        public Task HandleAsync(string taskId, JsonNode node)
        {
            var nodesForChange = templateJsonNodeService.Find(node);
            randomJsonValueService.GenerateRandomValues(nodesForChange);

            return Task.CompletedTask;
        }
    }
}
