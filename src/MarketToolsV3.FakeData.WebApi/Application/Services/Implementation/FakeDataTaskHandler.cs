using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskHandler(ILogger<FakeDataTaskHandler> logger,
        INotificationService notificationService)
        : IFakeDataTaskHandler
    {
        public Task HandleAsync(FakeDataTaskNotification notification)
        {
            logger.LogInformation("Handle {@notification}", notification);

            return Task.CompletedTask;
        }
    }
}
