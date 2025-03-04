using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class TaskDetailsService : ITaskDetailsService
    {
        public void IncrementScore(bool isSuccess, TaskDetailsEntity taskDetails)
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

        public void SetState(TaskDetailsEntity taskDetails)
        {
            bool isEndBySuccess = taskDetails.TaskEndCondition == TaskEndCondition.AwaitEqualsSuccess
                                  && taskDetails.NumSuccessful >= taskDetails.NumberOfExecutions;

            bool isEndSumSuccessAndFail = taskDetails.TaskEndCondition == TaskEndCondition.AwaitEqualsSumSuccessAndFail
                                          && taskDetails.NumSuccessful + taskDetails.NumFailed >= taskDetails.NumberOfExecutions;

            bool isEnd = isEndBySuccess || isEndSumSuccessAndFail;

            if (isEnd == false)
            {
                return;
            }

            if (taskDetails.TaskCompleteCondition == TaskCompleteCondition.SuccessEqualsExecution)
            {
                taskDetails.State = taskDetails.NumSuccessful == taskDetails.NumberOfExecutions 
                    ? TaskDetailsState.Complete 
                    : TaskDetailsState.Fail;
                return;
            }

            if (taskDetails.TaskCompleteCondition == TaskCompleteCondition.AnyResult)
            {
                taskDetails.State = TaskDetailsState.Complete;
                return;
            }

            taskDetails.State = TaskDetailsState.Undefined;

        }
    }
}
