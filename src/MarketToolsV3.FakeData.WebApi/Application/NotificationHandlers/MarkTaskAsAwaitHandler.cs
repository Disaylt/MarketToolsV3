using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class MarkTaskAsAwaitHandler(IUnitOfWork unitOfWork,
        IRepository<FakeDataTaskEntity> fakeDataTaskRepository,
        IPublisher<RunFakeDataTaskNotification> runFakeDataTaskPublisher)
        : INotificationHandler<MarkTaskAsAwaitNotification>
    {
        public async Task HandleAsync(MarkTaskAsAwaitNotification notification)
        {
            FakeDataTaskEntity taskEntityEntity = await fakeDataTaskRepository.FindRequiredAsync(notification.Id);

            taskEntityEntity.State = TaskState.AwaitRun;
            await unitOfWork.SaveChangesAsync();

            await NotifyRunAsync(notification.Id);
        }

        private async Task NotifyRunAsync(string id)
        {
            RunFakeDataTaskNotification notification = new()
            {
                TaskId = id
            };

            await runFakeDataTaskPublisher.Notify(notification);
        }
    }
}
