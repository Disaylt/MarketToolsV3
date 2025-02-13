using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class ProcessTaskDetailsHandler(IPublisher<StateTaskDetailsNotification> stateTaskDetailsPublisher)
    : INotificationHandler<HandleTaskDetailsNotification>
    {
        public async Task HandleAsync(HandleTaskDetailsNotification notification)
        {
            StateTaskDetailsNotification stateNotification = new()
            {
                TaskDetailsId = notification.TaskDetailsId,
                Success = true
            };

            try
            {
                int random = Random.Shared.Next(1, 10);

                if (random > 7)
                {
                    throw new Exception("Bad random =)");
                }
            }
            catch
            {
                stateNotification.Success = false;
            }
            finally
            {
                await stateTaskDetailsPublisher.Notify(stateNotification);
            }
        }
    }
}
