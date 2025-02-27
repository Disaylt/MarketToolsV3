using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Abstract;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation
{
    public class TaskDetailsHttpBodyService : ITaskDetailsHttpBodyService
    {
        public async Task<HttpRequestMessage> CreateRequestMessage(TaskDetails taskDetails)
        {
            HttpMethod httpMethod = HttpMethod.Parse(taskDetails.Method);
            HttpRequestMessage httpRequestMessage = new(httpMethod, taskDetails.Path);
            if (string.IsNullOrEmpty(taskDetails.JsonBody) == false)
            {
                JsonNode? json = JsonNode.Parse(taskDetails.JsonBody);
                httpRequestMessage.Content = JsonContent.Create(json);
            }

            return httpRequestMessage;
        }
    }
}
