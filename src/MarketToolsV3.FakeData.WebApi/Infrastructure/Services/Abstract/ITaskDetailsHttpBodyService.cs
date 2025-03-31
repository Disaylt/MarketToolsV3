using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Abstract
{
    public interface ITaskDetailsHttpBodyService
    {
        Task<HttpRequestMessage> CreateRequestMessage(TaskDetailsEntity taskDetails);
    }
}
