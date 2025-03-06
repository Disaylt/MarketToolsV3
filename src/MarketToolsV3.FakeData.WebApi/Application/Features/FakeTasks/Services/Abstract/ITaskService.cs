using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.FakeTasks.Services.Abstract
{
    public interface ITaskService
    {
        Task<TaskDto> CreateAsync(IReadOnlyCollection<NewTaskDetailsDto> tasks);
        Task SetState(string id, TaskState state);
        Task<IReadOnlyCollection<TaskDto>> GetAllAsync();
    }
}
