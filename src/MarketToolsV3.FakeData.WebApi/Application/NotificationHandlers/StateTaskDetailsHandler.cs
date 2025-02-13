using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class StateTaskDetailsHandler(IPublisher<TimeoutTaskDetailsNotification> timeoutTaskDetailsPublisher)
    : INotificationHandler<StateTaskDetailsNotification>
    {
        public async Task HandleAsync(StateTaskDetailsNotification notification)
        {
            int time = Random.Shared.Next(1, 3) * 1000;

            await Task.Delay(time);

            TimeoutTaskDetailsNotification timeoutNotification = new()
            {
                TaskDetailsId = notification.TaskDetailsId
            };

            await timeoutTaskDetailsPublisher.Notify(timeoutNotification);
        }
    }
}
