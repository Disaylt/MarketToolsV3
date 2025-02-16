using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface ITaskDetailsService
    {
        void IncrementScore(bool isSuccess, TaskDetails taskDetails);
        void SetState(TaskDetails taskDetails);
    }
}
