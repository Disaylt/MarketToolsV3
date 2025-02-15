using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using System;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class SelectTaskDetailsHandler(IPublisher<TimeoutTaskDetailsNotification> timeoutTaskDetailsNotificationPublisher,
        IUnitOfWork unitOfWork)
    : INotificationHandler<SelectTaskDetailsNotification>
    {
        public async Task HandleAsync(SelectTaskDetailsNotification notification)
        {

            TimeoutTaskDetailsNotification handleNotification = new()
            {
                TaskDetailsId = randomId
            };

            await timeoutTaskDetailsNotificationPublisher.Notify(handleNotification);
        }
    }
}
