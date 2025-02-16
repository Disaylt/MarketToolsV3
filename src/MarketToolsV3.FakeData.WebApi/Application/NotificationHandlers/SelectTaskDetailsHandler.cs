using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using System;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class SelectTaskDetailsHandler(IPublisher<TimeoutTaskDetailsNotification> timeoutTaskDetailsNotificationPublisher,
        ITaskDetailsEntityService fakeDataDetailsEntityService)
    : INotificationHandler<SelectTaskDetailsNotification>
    {
        public async Task HandleAsync(SelectTaskDetailsNotification notification)
        {
            TaskDetails? taskDetails = await fakeDataDetailsEntityService.TakeNextAsync();

            if (taskDetails is null)
            {
                return;
            }

            TimeoutTaskDetailsNotification handleNotification = new()
            {
                TaskDetailsId = taskDetails.Id
            };

            await timeoutTaskDetailsNotificationPublisher.Notify(handleNotification);
        }
    }
}
