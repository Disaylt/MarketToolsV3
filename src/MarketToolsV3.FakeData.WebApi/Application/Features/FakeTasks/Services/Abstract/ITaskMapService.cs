using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.FakeTasks.Services.Abstract
{
    public interface ITaskMapService
    {
        TaskEntity Map(IReadOnlyCollection<NewTaskDetailsDto> tasksDetails);
    }
}
