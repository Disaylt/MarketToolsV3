using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.TaskDetails.Services.Abstract
{
    public interface ITaskDetailsService
    {
        void IncrementScore(bool isSuccess, TaskDetailsEntity taskDetails);
        void SetState(TaskDetailsEntity taskDetails);
    }
}
