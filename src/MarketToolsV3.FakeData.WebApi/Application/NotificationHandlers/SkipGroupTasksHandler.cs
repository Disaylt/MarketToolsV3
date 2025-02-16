using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class SkipGroupTasksHandler(IRepository<TaskDetails> taskDetailsRepository,
        ITaskDetailsEntityService taskDetailsEntityService)
        : INotificationHandler<SkipGroupTasksNotification>
    {
        public async Task HandleAsync(SkipGroupTasksNotification notification)
        {
            TaskDetails taskDetails = await taskDetailsRepository.FindRequiredAsync(notification.TaskDetailsId);
            await taskDetailsEntityService.SetAsSkipGroupAsync(taskDetails.TaskId, taskDetails.NumGroup);

            throw new NotImplementedException();
        }
    }
}
