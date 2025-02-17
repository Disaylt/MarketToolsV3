﻿using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers
{
    public class RunFakeDataTaskHandler(IUnitOfWork unitOfWork,
        ILogger<RunFakeDataTaskHandler> logger,
        IFakeDataTaskEntityService fakeDataTaskEntityService,
        IPublisher<SelectTaskDetailsNotification> selectTaskDetailsPublisher,
        IPublisher<FailFakeDataTasksNotification> fakeDataTasksFailHandlingPublisher)
        : INotificationHandler<RunFakeDataTaskNotification>
    {
        public async Task HandleAsync(RunFakeDataTaskNotification notification)
        {
            try
            {
                FakeDataTask? taskEntity = await fakeDataTaskEntityService.FindAsync(notification.TaskId);

                if (taskEntity is not { State: TaskState.AwaitRun })
                {
                    logger.LogInformation("Task not found or status is not 'await'");

                    return;
                }

                taskEntity.State = TaskState.InProcess;
                await unitOfWork.SaveChangesAsync();

                SelectTaskDetailsNotification selectNotification = new()
                {
                    TaskId = notification.TaskId
                };

                await selectTaskDetailsPublisher.Notify(selectNotification);
            }
            catch
            {
                FailFakeDataTasksNotification failNotification = new()
                {
                    Id = notification.TaskId
                };
                await fakeDataTasksFailHandlingPublisher.Notify(failNotification);
            }
        }
    }
}
