using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class SelectTaskDetailsHandler(IPublisher<HandleTaskDetailsNotification> handleTaskDetailsPublisher)
    : INotificationHandler<SelectTaskDetailsNotification>
    {
        public async Task HandleAsync(SelectTaskDetailsNotification notification)
        {
            int randomId = Random.Shared.Next(1, 123);

            HandleTaskDetailsNotification handleNotification = new()
            {
                TaskDetailsId = randomId
            };

            await handleTaskDetailsPublisher.Notify(handleNotification);
        }
    }
}
