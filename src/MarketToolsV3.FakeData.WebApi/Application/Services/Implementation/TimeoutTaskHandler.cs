using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class TimeoutTaskHandler(ILogger<TimeoutTaskHandler> logger,
        IPublisher<FakeDataTaskNotification> fakeDataTaskPublisher)
    : INotificationHandler<TimeoutNotification>
    {
        public async Task HandleAsync(TimeoutNotification notification)
        {
            if (notification.Milliseconds > 0)
            {
                await Task.Delay(notification.Milliseconds);
            }

            FakeDataTaskNotification fakeDataTaskNotification = CreateNextNotification(notification.TaskId);

            await fakeDataTaskPublisher.Notify(fakeDataTaskNotification);
        }

        private FakeDataTaskNotification CreateNextNotification(int taskId)
        {
            return new()
            {
                TaskId = taskId
            };
        }
    }
}
