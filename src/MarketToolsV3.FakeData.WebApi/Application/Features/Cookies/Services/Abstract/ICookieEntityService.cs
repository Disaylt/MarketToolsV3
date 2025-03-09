using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.Cookies.Services.Abstract
{
    public interface ICookieEntityService
    {
        Task<IReadOnlyCollection<CookieEntity>> GetRangeByTaskAsync(string taskId);
        Task AddRangeAsync(IEnumerable<CookieEntity> entities);
        Task ClearByTaskIdAsync(string id);
    }
}
