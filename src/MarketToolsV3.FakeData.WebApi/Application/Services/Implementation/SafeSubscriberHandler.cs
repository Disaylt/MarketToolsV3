using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class SafeSubscriberHandler(ILogger<SafeSubscriberHandler> logger) 
        : ISafeSubscriberHandler
    {
        public async Task SafeRunAsync<T>(ISubscriber<T> subscriber, T notification) where T : BaseNotification
        {
            try
            {
                logger.LogInformation("Run subscriber type:{subscriberType}. Notification - {@notification}",
                    subscriber.GetType().Name,
                    notification);

                await subscriber.HandleAsync(notification);

                logger.LogInformation("Subscriber was handle");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Subscriber handling error.");
            }
        }
    }
}
