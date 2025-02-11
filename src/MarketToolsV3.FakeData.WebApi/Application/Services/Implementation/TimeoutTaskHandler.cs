using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class TimeoutTaskHandler(ILogger<TimeoutTaskHandler> logger,
        IPublisher<FakeDataTaskNotification> fakeDataTaskPublisher)
    : INotificationHandler<TimeoutNotification>
    {
        public async Task HandleAsync(TimeoutNotification notification)
        {
            logger.LogInformation("Handle {@notification}", notification);

            await Task.Delay(notification.Milliseconds);

            FakeDataTaskNotification fakeDataTaskNotification = new()
            {
                TaskId = notification.TaskId
            };

            await fakeDataTaskPublisher.Notify(fakeDataTaskNotification);
        }
    }
}
