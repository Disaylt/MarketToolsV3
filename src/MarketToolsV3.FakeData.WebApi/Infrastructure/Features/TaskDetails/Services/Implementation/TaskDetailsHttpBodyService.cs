using MarketToolsV3.FakeData.WebApi.Application.Features.Json.Handlers.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Abstract;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Features.TaskDetails.Services.Implementation
{
    public class TaskDetailsHttpBodyService(IEnumerable<IJsonNodeHandler> jsonNodeHandlers)
        : ITaskDetailsHttpBodyService
    {
        public async Task<HttpRequestMessage> CreateRequestMessage(TaskDetailsEntity taskDetails)
        {
            HttpMethod httpMethod = HttpMethod.Parse(taskDetails.Method);
            HttpRequestMessage httpRequestMessage = new(httpMethod, taskDetails.Path);

            JsonNode? node = null;
            if (string.IsNullOrEmpty(taskDetails.JsonBody) == false)
            {
                node = JsonNode.Parse(taskDetails.JsonBody);
            }

            if (node != null)
            {
                await HandleNodeAsync(node, taskDetails.TaskId);
                httpRequestMessage.Content = JsonContent.Create(node);
            }

            return httpRequestMessage;
        }

        private async Task HandleNodeAsync(JsonNode node, string taskId)
        {
            foreach (var jsonNodeHandler in jsonNodeHandlers)
            {
                await jsonNodeHandler.HandleAsync(taskId, node);
            }
        }
    }
}
