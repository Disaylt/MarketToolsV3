using System.Text.Json.Nodes;
using MarketToolsV3.FakeData.WebApi.Application.Enums;
using MarketToolsV3.FakeData.WebApi.Application.Features.Json.Handlers.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Features.Json.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.Json.Handlers.Implementation
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
