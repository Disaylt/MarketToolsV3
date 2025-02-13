using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class TimeoutTaskDetailsHandler(IPublisher<FakeDataTaskNotification> fakeDataTaskPublisher)
    : INotificationHandler<TimeoutTaskDetailsNotification>
    {
        public async Task HandleAsync(TimeoutTaskDetailsNotification notification)
        {
            int time = Random.Shared.Next(1, 10) * 1000;

            await Task.Delay(time);

            FakeDataTaskNotification fakeDataTaskNotification = CreateNextNotification(1);

            await fakeDataTaskPublisher.Notify(fakeDataTaskNotification);
        }

        private static FakeDataTaskNotification CreateNextNotification(int taskId)
        {
            return new()
            {
                TaskId = taskId
            };
        }
    }
}
