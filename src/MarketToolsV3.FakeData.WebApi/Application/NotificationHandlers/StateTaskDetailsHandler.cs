using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Services.Implementation;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class StateTaskDetailsHandler(IPublisher<SkipGroupTasksNotification> fakeDataTaskNotificationPublisher,
        IRepository<TaskDetails> taskDetailsRepository,
        ITaskDetailsService taskDetailsService,
        IUnitOfWork unitOfWork)
    : INotificationHandler<StateTaskDetailsNotification>
    {
        public async Task HandleAsync(StateTaskDetailsNotification notification)
        {
            TaskDetails taskDetails = await taskDetailsRepository.FindRequiredAsync(notification.TaskDetailsId);
            taskDetailsService.IncrementScore(notification.Success, taskDetails);
            taskDetailsService.SetState(taskDetails);

            await unitOfWork.SaveChangesAsync();

            SkipGroupTasksNotification taskNotification = CreateNextNotification(taskDetails);

            await fakeDataTaskNotificationPublisher.Notify(taskNotification);
        }

        private SkipGroupTasksNotification CreateNextNotification(TaskDetails taskDetails)
        {
            return new()
            {
                TaskDetailsId = taskDetails.Id
            };
        }
    }
}
