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

            RunFakeDataTaskNotification fakeDataNotification = new()
            {
                TaskId = notification.Id
            };

            await fakeDataTaskPublisher.Notify(fakeDataNotification);
        }
    }
}
