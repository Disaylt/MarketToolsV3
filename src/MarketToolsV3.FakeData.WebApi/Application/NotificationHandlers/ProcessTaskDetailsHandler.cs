using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class ProcessTaskDetailsHandler(IPublisher<StateTaskDetailsNotification> stateTaskDetailsPublisher,
        ILogger<ProcessTaskDetailsHandler> logger,
        ITaskDetailsHandleFacadeService taskDetailsHandleFacadeService)
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
                await taskDetailsHandleFacadeService.HandleAsync(notification.TaskDetailsId);
            }
            catch(Exception ex)
            {
                stateNotification.Success = false;
                logger.LogWarning(ex, "Message: {message}", ex.Message);
            }
            finally
            {
                await stateTaskDetailsPublisher.Notify(stateNotification);
            }
        }
    }
}
