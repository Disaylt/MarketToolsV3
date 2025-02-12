using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class FakeDataTasksFailHandler(IPublisher<FakeDataTaskNotification> fakeDataTaskPublisher)
        : INotificationHandler<FakeDataTasksFailNotification>
    {
        public async Task HandleAsync(FakeDataTasksFailNotification notification)
        {
            await Task.Delay(TimeSpan.FromMinutes(1));

            FakeDataTaskNotification fakeDataNotification = new()
            {
                TaskId = notification.Id
            };

            await fakeDataTaskPublisher.Notify(fakeDataNotification);
        }
    }
}
