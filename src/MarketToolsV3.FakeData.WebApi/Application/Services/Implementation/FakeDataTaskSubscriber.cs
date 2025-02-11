using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskSubscriber(IServiceProvider serviceProvider)
        : ISubscriber<FakeDataTaskNotification>
    {
        public async Task HandleAsync(FakeDataTaskNotification notification)
        {
            using var scope = serviceProvider.CreateScope();

            await scope.ServiceProvider
                .GetRequiredService<INotificationHandler<FakeDataTaskNotification>>()
                .HandleAsync(notification);
        }
    }
}
