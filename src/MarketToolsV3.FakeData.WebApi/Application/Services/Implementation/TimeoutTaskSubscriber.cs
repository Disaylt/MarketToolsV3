using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class TimeoutTaskSubscriber(IServiceProvider serviceProvider)
        : ISubscriber<TimeoutNotification>
    {
        public async Task HandleAsync(TimeoutNotification notification)
        {
            using var scope = serviceProvider.CreateScope();

            await scope.ServiceProvider
                .GetRequiredService<INotificationHandler<TimeoutNotification>>()
                .HandleAsync(notification);
        }
    }
}
