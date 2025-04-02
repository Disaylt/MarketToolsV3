using MarketToolsV3.FakeData.WebApi.Application.Enums;
using MarketToolsV3.FakeData.WebApi.Application.Features.ResponseBody.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Utilities.Abstract;
using System.Text.Json.Nodes;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Application.Features.Json.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Features.Json.Handlers.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Features.Json.Utilities.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.Json.Handlers.Implementation
{
    public class BodySelectJsonNodeHandler(
        [FromKeyedServices(TemplateJsonNode.SelectBody)]
        ITemplateJsonNodeService templateJsonNodeService,
        ISelectBodyTemplatePattern selectBodyTemplatePattern,
        IResponseBodyTagService responseBodyTagService,
        ITagService tagService,
        IJsonNodeParser jsonNodeParser)
        : IJsonNodeHandler
    {
        public async Task HandleAsync(string taskId, JsonNode node)
        {
            ICollection<JsonValue> values = templateJsonNodeService.Find(node);
            foreach (var value in values)
            {
                string data = value.GetValue<string>();
                SelectBodyTemplateModel template = selectBodyTemplatePattern.Parse(data);
                TagTemplateModel tagTemplate = tagService.Parse(template.Tag);
                ResponseBodyEntity responseBody = await responseBodyTagService.SelectByTemplateAsync(taskId, tagTemplate);

                string jsonBody = responseBody.Data ?? throw new NullReferenceException("Data is null in response body");
                JsonNode bodyNode = JsonNode.Parse(jsonBody) ?? throw new NullReferenceException("Bad json node parse");
                JsonNode? targetNode = jsonNodeParser.ParseByPath(bodyNode, template.Paths);

                value.ReplaceWith(targetNode);
            }
        }
    }
}
