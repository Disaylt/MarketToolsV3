using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface IFakeDataDetailsEntityService
    {
        Task<TaskDetails?> TakeNextAsync();
    }
}
