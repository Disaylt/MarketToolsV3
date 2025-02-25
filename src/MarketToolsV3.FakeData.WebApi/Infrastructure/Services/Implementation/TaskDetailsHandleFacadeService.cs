using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation
{
    public class TaskDetailsHandleFacadeService(
        IRepository<TaskDetails> taskDetailsRepository,
        ITaskHttpClientFactory taskHttpClientFactory,
        IUnitOfWork unitOfWork)
        : ITaskDetailsHandleFacadeService
    {
        public async Task HandleAsync(int id)
        {
            TaskDetails taskDetails = await taskDetailsRepository.FindRequiredAsync(id);
            HttpRequestMessage httpRequestMessage = CreateRequestMessage(taskDetails);

            ITaskHttpClient taskHttpClient = await taskHttpClientFactory.CreateAsync(taskDetails.TaskId);
            HttpResponseMessage responseMessage = await taskHttpClient.SendAsync(httpRequestMessage, CancellationToken.None);
            responseMessage.EnsureSuccessStatusCode();

            ResponseBody response = await CreateResponseBody(responseMessage, id);
            taskDetails.Responses.Add(response);

            await unitOfWork.SaveChangesAsync();
        }

        private static async Task<ResponseBody> CreateResponseBody(HttpResponseMessage responseMessage, int taskDetailsId)
        {
            return new()
            {
                Data = await responseMessage.Content.ReadAsStringAsync(),
                StatusCode = responseMessage.StatusCode,
                TaskDetailsId = taskDetailsId
            };
        }

        private static HttpRequestMessage CreateRequestMessage(TaskDetails taskDetails)
        {
            HttpMethod httpMethod = HttpMethod.Parse(taskDetails.Method);
            HttpRequestMessage httpRequestMessage = new(httpMethod, taskDetails.Path);
            if (string.IsNullOrEmpty(taskDetails.JsonBody) == false)
            {
                httpRequestMessage.Content = JsonContent.Create(taskDetails.JsonBody);
            }

            return httpRequestMessage;
        }
    }
}
