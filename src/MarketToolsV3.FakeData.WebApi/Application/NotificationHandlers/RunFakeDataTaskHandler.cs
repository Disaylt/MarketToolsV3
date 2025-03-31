using MarketToolsV3.FakeData.WebApi.Application.Features.FakeTasks.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class RunFakeDataTaskHandler(IUnitOfWork unitOfWork,
        ILogger<RunFakeDataTaskHandler> logger,
        ITaskEntityService fakeDataTaskEntityService,
        IPublisher<SelectTaskDetailsNotification> selectTaskDetailsPublisher,
        IPublisher<FailFakeDataTasksNotification> fakeDataTasksFailHandlingPublisher)
        : INotificationHandler<RunFakeDataTaskNotification>
    {
        public async Task HandleAsync(RunFakeDataTaskNotification notification)
        {
            try
            {
                TaskEntity? taskEntity = await fakeDataTaskEntityService.FindAsync(notification.TaskId);

                if (taskEntity is not { State: TaskState.AwaitRun })
                {
                    logger.LogInformation("TaskEntity not found or status is not 'await'");

                    return;
                }

                taskEntity.State = TaskState.InProcess;
                await unitOfWork.SaveChangesAsync();

                await NotifySelectDetailsAsync(notification.TaskId);
            }
            catch
            {
                await NotifyFailAsync(notification.TaskId);
            }
        }

        private async Task NotifySelectDetailsAsync(string id)
        {
            SelectTaskDetailsNotification notification = new()
            {
                TaskId = id
            };

            await selectTaskDetailsPublisher.Notify(notification);

        }

        private async Task NotifyFailAsync(string id)
        {
            FailFakeDataTasksNotification notification = new()
            {
                Id = id
            };
            await fakeDataTasksFailHandlingPublisher.Notify(notification);
        }
    }
}
