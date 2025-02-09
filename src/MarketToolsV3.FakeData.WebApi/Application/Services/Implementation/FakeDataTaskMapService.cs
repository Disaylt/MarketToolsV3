using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskMapService : IFakeDataTaskMapService
    {
        public FakeDataTask Map(IReadOnlyCollection<NewFakeDataTaskDto> tasksDetails)
        {
            FakeDataTask taskEntity = CreateTask();

            int sortIndex = 0;

            foreach (NewFakeDataTaskDto taskDetails in tasksDetails)
            {
                TaskDetails taskDetailsEntity = CreateDetails(taskDetails, sortIndex);

                taskEntity.Details.Add(taskDetailsEntity);
                sortIndex++;
            }

            return taskEntity;
        }

        private TaskDetails CreateDetails(NewFakeDataTaskDto taskDetails, int sortIndex)
        {
            return new()
            {
                Path = taskDetails.Path ?? throw new Exception("Path is null"),
                Tag = taskDetails.Tag,
                Method = taskDetails.Method,
                JsonBody = taskDetails.Body?.ToJsonString(),
                TaskCompleteCondition = taskDetails.TaskCompleteCondition,
                NumberOfExecutions = taskDetails.NumberOfExecutions,
                NumGroup = taskDetails.NumGroup,
                TimeoutBeforeRun = taskDetails.TimeoutBeforeRun,
                SortIndex = sortIndex
            };
        }

        private FakeDataTask CreateTask()
        {
            return new()
            {
                State = TaskState.AwaitRun
            };
        }
    }
}
