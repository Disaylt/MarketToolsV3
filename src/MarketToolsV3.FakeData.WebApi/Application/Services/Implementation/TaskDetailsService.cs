using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class TaskDetailsService : ITaskDetailsService
    {
        public void IncrementScore(bool isSuccess, TaskDetails taskDetails)
        {
            if (isSuccess)
            {
                taskDetails.NumSuccessful += 1;
            }
            else
            {
                taskDetails.NumFailed += 1;
            }
        }

        public void SetState(TaskDetails taskDetails)
        {
            if (taskDetails.TaskEndCondition == TaskEndCondition.HandleTotalQuantity
                && taskDetails.NumFailed + taskDetails.NumSuccessful >= taskDetails.NumberOfExecutions)
            {
                taskDetails.State = 
            }
        }
    }
}
