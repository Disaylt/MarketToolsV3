using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class TimeoutTaskSubscriber(IServiceProvider serviceProvider)
        : ISubscriber<TimeoutTaskDetailsNotification>
    {
        public async Task HandleAsync(TimeoutTaskDetailsNotification notification)
        {
            using var scope = serviceProvider.CreateScope();

            await scope.ServiceProvider
                .GetRequiredService<INotificationHandler<TimeoutTaskDetailsNotification>>()
                .HandleAsync(notification);
        }
    }
}
