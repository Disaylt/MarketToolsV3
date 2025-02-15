using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class StateTaskDetailsHandler(IPublisher<FakeDataTaskNotification> fakeDataTaskNotificationPublisher,
        IRepository<TaskDetails> taskDetailsRepository)
    : INotificationHandler<StateTaskDetailsNotification>
    {
        public async Task HandleAsync(StateTaskDetailsNotification notification)
        {
            TaskDetails taskDetails = await taskDetailsRepository.FindRequiredAsync(notification.TaskDetailsId);


            FakeDataTaskNotification timeoutNotification = new()
            {
                TaskId = taskDetails.TaskId
            };

            await fakeDataTaskNotificationPublisher.Notify(timeoutNotification);
        }
    }
}
