using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class MarkAsHandleHandler(IUnitOfWork unitOfWork,
        IRepository<TaskEntity> fakeDataTaskRepository)
        : INotificationHandler<MarkAsHandleNotification>
    {
        public async Task HandleAsync(MarkAsHandleNotification notification)
        {
            TaskEntity taskEntityEntity = await fakeDataTaskRepository.FindRequiredAsync(notification.Id);

            taskEntityEntity.State = TaskState.Handled;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
