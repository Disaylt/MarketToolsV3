using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class ProcessTaskDetailsHandler(IPublisher<StateTaskDetailsNotification> stateTaskDetailsPublisher,
        ILogger<ProcessTaskDetailsHandler> logger)
    : INotificationHandler<ProcessTaskDetailsNotification>
    {
        public async Task HandleAsync(ProcessTaskDetailsNotification notification)
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
            catch(Exception ex)
            {
                stateNotification.Success = false;
                logger.LogWarning(ex, ex.Message);
            }
            finally
            {
                await stateTaskDetailsPublisher.Notify(stateNotification);
            }
        }
    }
}
