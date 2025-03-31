using MarketToolsV3.FakeData.WebApi.Application.Features.TaskDetails.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Services.Implementation;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class StateTaskDetailsHandler(IPublisher<SkipGroupTasksNotification> fakeDataTaskNotificationPublisher,
        IRepository<TaskDetailsEntity> taskDetailsRepository,
        ITaskDetailsService taskDetailsService,
        IUnitOfWork unitOfWork)
    : INotificationHandler<StateTaskDetailsNotification>
    {
        public async Task HandleAsync(StateTaskDetailsNotification notification)
        {
            TaskDetailsEntity taskDetails = await taskDetailsRepository.FindRequiredAsync(notification.TaskDetailsId);
            taskDetailsService.IncrementScore(notification.Success, taskDetails);
            taskDetailsService.SetState(taskDetails);

            await unitOfWork.SaveChangesAsync();

            await NotifySkipGroupAsync(taskDetails.Id);
        }

        private async Task NotifySkipGroupAsync(int id)
        {
            SkipGroupTasksNotification taskNotification = new()
            {
                TaskDetailsId = id
            };

            await fakeDataTaskNotificationPublisher.Notify(taskNotification);
        }
    }
}
