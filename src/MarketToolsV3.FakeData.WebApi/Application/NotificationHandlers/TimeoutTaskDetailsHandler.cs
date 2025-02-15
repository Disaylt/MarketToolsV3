using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class TimeoutTaskDetailsHandler(IPublisher<HandleTaskDetailsNotification> handleTaskDetailsNotificationPublisher,
        IRepository<TaskDetails> taskDetailsRepository)
    : INotificationHandler<TimeoutTaskDetailsNotification>
    {
        public async Task HandleAsync(TimeoutTaskDetailsNotification notification)
        {
            TaskDetails taskDetails = await taskDetailsRepository.FindRequiredAsync(notification.TaskDetailsId);

            await AwaitTaskTimeout(taskDetails);

            HandleTaskDetailsNotification fakeDataTaskNotification = CreateNextNotification(1);

            await handleTaskDetailsNotificationPublisher.Notify(fakeDataTaskNotification);
        }

        private static async Task AwaitTaskTimeout(TaskDetails taskDetails)
        {
            if (taskDetails.TimeoutBeforeRun > 0)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(taskDetails.TimeoutBeforeRun));
            }
        }

        private static HandleTaskDetailsNotification CreateNextNotification(int taskId)
        {
            return new()
            {
                TaskDetailsId = taskId
            };
        }
    }
}
