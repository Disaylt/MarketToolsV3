using MarketToolsV3.FakeData.WebApi.Application.Features.TaskDetails.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Abstract;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Features.TaskDetails.Services.Implementation
{
    public class TaskDetailsHandleFacadeService(
        IRepository<TaskDetailsEntity> taskDetailsRepository,
        ITaskHttpClientFactory taskHttpClientFactory,
        ITaskDetailsHttpBodyService taskDetailsHttpBodyService,
        IUnitOfWork unitOfWork)
        : ITaskDetailsHandleFacadeService
    {
        public async Task HandleAsync(int id)
        {
            TaskDetailsEntity taskDetails = await taskDetailsRepository.FindRequiredAsync(id);
            HttpRequestMessage httpRequestMessage = await taskDetailsHttpBodyService.CreateRequestMessage(taskDetails);

            ITaskHttpClient taskHttpClient = await taskHttpClientFactory.CreateAsync(taskDetails.TaskId);
            HttpResponseMessage responseMessage = await taskHttpClient.SendAsync(httpRequestMessage, CancellationToken.None);


            responseMessage.EnsureSuccessStatusCode();

            ResponseBodyEntity response = await CreateResponseBody(responseMessage, id);
            taskDetails.Responses.Add(response);

            await unitOfWork.SaveChangesAsync();
        }

        private static async Task<ResponseBodyEntity> CreateResponseBody(HttpResponseMessage responseMessage, int taskDetailsId)
        {
            return new()
            {
                Data = await responseMessage.Content.ReadAsStringAsync(),
                StatusCode = responseMessage.StatusCode,
                TaskDetailId = taskDetailsId
            };
        }
    }
}
