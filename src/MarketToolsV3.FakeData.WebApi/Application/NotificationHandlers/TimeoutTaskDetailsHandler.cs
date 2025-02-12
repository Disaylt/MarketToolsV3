using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class TimeoutTaskDetailsHandler(ILogger<TimeoutTaskDetailsHandler> logger,
        IPublisher<FakeDataTaskNotification> fakeDataTaskPublisher)
    : INotificationHandler<TimeoutTaskDetailsNotification>
    {
        public async Task HandleAsync(TimeoutTaskDetailsNotification notification)
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
