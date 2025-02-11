using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskHandler(ILogger<FakeDataTaskHandler> logger,
        IPublisher<TimeoutNotification> timeoutPublisher)
        : INotificationHandler<FakeDataTaskNotification>
    {
        public async Task HandleAsync(FakeDataTaskNotification notification)
        {
            logger.LogInformation("Handle {@notification}", notification);

            TimeoutNotification timeoutNotification = new()
            {
                Milliseconds = Random.Shared.Next(1, 5) * 1000,
                TaskId = notification.TaskId
            };

            await timeoutPublisher.Notify(timeoutNotification);
        }
    }
}
