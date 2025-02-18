using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class MarkAsHandleHandler(IUnitOfWork unitOfWork,
        IRepository<FakeDataTask> fakeDataTaskRepository)
        : INotificationHandler<MarkAsHandleNotification>
    {
        public async Task HandleAsync(MarkAsHandleNotification notification)
        {
            FakeDataTask taskEntity = await fakeDataTaskRepository.FindRequiredAsync(notification.Id);

            taskEntity.State = TaskState.AwaitRun;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
