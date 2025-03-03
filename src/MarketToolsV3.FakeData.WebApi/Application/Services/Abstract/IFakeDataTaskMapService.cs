using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface IFakeDataTaskMapService
    {
        FakeDataTaskEntity Map(IReadOnlyCollection<NewFakeDataTaskDto> tasksDetails);
    }
}
