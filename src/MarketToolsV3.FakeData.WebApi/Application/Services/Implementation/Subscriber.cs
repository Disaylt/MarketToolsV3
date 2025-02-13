﻿using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class Subscriber<T>(IServiceProvider serviceProvider,
        ILogger<Subscriber<T>> logger)
        : ISubscriber<T> 
        where T : BaseNotification
    {
        public async Task HandleAsync(T notification)
        {
            logger.LogInformation("Handle notification type:{type}. Body: {@body}", typeof(T).FullName, notification);

            using var scope = serviceProvider.CreateScope();

            await scope
                .ServiceProvider
                .GetRequiredService<INotificationHandler<T>>()
                .HandleAsync(notification);

            logger.LogInformation("Finish handling type: {type}", typeof(T).FullName);
        }
    }
}
