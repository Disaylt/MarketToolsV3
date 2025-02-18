using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using System;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class SelectTaskDetailsHandler(IPublisher<TimeoutTaskDetailsNotification> timeoutTaskDetailsNotificationPublisher,
        IPublisher<MarkAsHandleNotification> _markAsHandlePublisher,
        ITaskDetailsEntityService fakeDataDetailsEntityService)
    : INotificationHandler<SelectTaskDetailsNotification>
    {
        public async Task HandleAsync(SelectTaskDetailsNotification notification)
        {
            TaskDetails? taskDetails = await fakeDataDetailsEntityService.TakeNextAsync();

            if (taskDetails is null)
            {
                await NotifyMarkAsHandleAsync(notification.TaskId);
            }
            else
            {
                await NotifyTimeoutAsync(taskDetails.Id);
            }
        }

        private async Task NotifyTimeoutAsync(int id)
        {
            TimeoutTaskDetailsNotification handleNotification = new()
            {
                TaskDetailsId = id
            };

            await timeoutTaskDetailsNotificationPublisher.Notify(handleNotification);
        }

        private async Task NotifyMarkAsHandleAsync(string id)
        {
            MarkAsHandleNotification notification = new()
            {
                Id = id
            };

            await _markAsHandlePublisher.Notify(notification);
        }
    }
}
