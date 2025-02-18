using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class SkipGroupTasksHandler(IRepository<TaskDetails> taskDetailsRepository,
        IPublisher<MarkTaskAsAwaitNotification> markTaskAsAwaitPublisher,
        ITaskDetailsEntityService taskDetailsEntityService)
        : INotificationHandler<SkipGroupTasksNotification>
    {
        public async Task HandleAsync(SkipGroupTasksNotification notification)
        {
            TaskDetails taskDetails = await taskDetailsRepository.FindRequiredAsync(notification.TaskDetailsId);

            if (taskDetails is { State: TaskDetailsState.Fail, NumGroup: not null })
            {
                await taskDetailsEntityService.SetGroupAsSkipAsync(taskDetails.TaskId, taskDetails.NumGroup.Value);
            }

            await NotifyMarkAsAwait(taskDetails.TaskId);
        }

        private async Task NotifyMarkAsAwait(string id)
        {
            MarkTaskAsAwaitNotification notification = new()
            {
                Id = id
            };

            await markTaskAsAwaitPublisher.Notify(notification);
        }
    }
}
