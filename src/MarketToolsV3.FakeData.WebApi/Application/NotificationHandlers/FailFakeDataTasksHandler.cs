using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class FailFakeDataTasksHandler(IPublisher<RunFakeDataTaskNotification> fakeDataTaskPublisher)
        : INotificationHandler<FailFakeDataTasksNotification>
    {
        public async Task HandleAsync(FailFakeDataTasksNotification notification)
        {
            await Task.Delay(TimeSpan.FromMinutes(1));

            await NotifyRunAsync(notification.Id);
        }

        private async Task NotifyRunAsync(string id)
        {
            RunFakeDataTaskNotification notification = new()
            {
                TaskId = id
            };

            await fakeDataTaskPublisher.Notify(notification);
        }
    }
}
