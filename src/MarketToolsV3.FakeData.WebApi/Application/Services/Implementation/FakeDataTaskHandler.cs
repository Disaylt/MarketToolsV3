using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskHandler(ILogger<FakeDataTaskHandler> logger,
        IFakeDataTaskEntityService fakeDataTaskEntityService,
        IPublisher<TimeoutNotification> timeoutPublisher)
        : INotificationHandler<FakeDataTaskNotification>
    {
        public async Task HandleAsync(FakeDataTaskNotification notification)
        {
            FakeDataTask? taskEntity = await fakeDataTaskEntityService.FindAsync(notification.TaskId);

            if (taskEntity is not { State: TaskState.AwaitRun })
            {
                return;
            }

            TimeoutNotification timeoutNotification = new()
            {
                TaskId = notification.TaskId
            };

            await timeoutPublisher.Notify(timeoutNotification);
        }
    }
}
