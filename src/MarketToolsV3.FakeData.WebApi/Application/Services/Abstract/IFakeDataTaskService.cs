using MarketToolsV3.FakeData.WebApi.Application.Models;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface IFakeDataTaskService
    {
        Task<FakeDataTaskDto> CreateAsync(IReadOnlyCollection<NewFakeDataTaskDto> tasks);
    }
}
