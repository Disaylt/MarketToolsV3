using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskMapService : ITaskMapService
    {
        public TaskEntity Map(IReadOnlyCollection<NewTaskDetailsDto> tasksDetails)
        {
            TaskEntity taskEntityEntity = CreateTask();

            int sortIndex = 0;

            foreach (NewTaskDetailsDto taskDetails in tasksDetails)
            {
                TaskDetailsEntity taskDetailsEntity = CreateDetails(taskDetails, sortIndex);

                taskEntityEntity.Details.Add(taskDetailsEntity);
                sortIndex++;
            }

            return taskEntityEntity;
        }

        private static TaskDetailsEntity CreateDetails(NewTaskDetailsDto taskDetailsDetails, int sortIndex)
        {
            return new()
            {
                Path = taskDetailsDetails.Path ?? throw new Exception("Path is null"),
                Tag = taskDetailsDetails.Tag,
                Method = taskDetailsDetails.Method,
                JsonBody = taskDetailsDetails.Body?.ToJsonString(),
                TaskEndCondition = taskDetailsDetails.TaskEndCondition,
                TaskCompleteCondition = taskDetailsDetails.TaskCompleteCondition,
                NumberOfExecutions = taskDetailsDetails.NumberOfExecutions,
                NumGroup = taskDetailsDetails.NumGroup,
                TimeoutBeforeRun = taskDetailsDetails.TimeoutBeforeRun,
                SortIndex = sortIndex
            };
        }

        private static TaskEntity CreateTask()
        {
            return new()
            {
                State = TaskState.AwaitRun
            };
        }
    }
}
