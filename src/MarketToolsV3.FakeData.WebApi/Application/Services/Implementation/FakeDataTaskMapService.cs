using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskMapService : IFakeDataTaskMapService
    {
        public FakeDataTaskEntity Map(IReadOnlyCollection<NewFakeDataTaskDto> tasksDetails)
        {
            FakeDataTaskEntity taskEntityEntity = CreateTask();

            int sortIndex = 0;

            foreach (NewFakeDataTaskDto taskDetails in tasksDetails)
            {
                TaskDetailsEntity taskDetailsEntity = CreateDetails(taskDetails, sortIndex);

                taskEntityEntity.Details.Add(taskDetailsEntity);
                sortIndex++;
            }

            return taskEntityEntity;
        }

        private static TaskDetailsEntity CreateDetails(NewFakeDataTaskDto taskDetails, int sortIndex)
        {
            return new()
            {
                Path = taskDetails.Path ?? throw new Exception("Path is null"),
                Tag = taskDetails.Tag,
                Method = taskDetails.Method,
                JsonBody = taskDetails.Body?.ToJsonString(),
                TaskEndCondition = taskDetails.TaskEndCondition,
                TaskCompleteCondition = taskDetails.TaskCompleteCondition,
                NumberOfExecutions = taskDetails.NumberOfExecutions,
                NumGroup = taskDetails.NumGroup,
                TimeoutBeforeRun = taskDetails.TimeoutBeforeRun,
                SortIndex = sortIndex
            };
        }

        private static FakeDataTaskEntity CreateTask()
        {
            return new()
            {
                State = TaskState.AwaitRun
            };
        }
    }
}
