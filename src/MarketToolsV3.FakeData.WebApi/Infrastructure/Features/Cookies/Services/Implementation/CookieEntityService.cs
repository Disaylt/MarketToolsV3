using MarketToolsV3.FakeData.WebApi.Application.Features.Cookies.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Features.Cookies.Services.Implementation
{
    public class CookieEntityService(
        FakeDataDbContext dbContext,
        IQueryableRepository<CookieEntity> cookieQueryableRepository)
        : ICookieEntityService
    {
        public async Task AddRangeAsync(IEnumerable<CookieEntity> entities)
        {
            await dbContext.Cookies.AddRangeAsync(entities);
        }

        public async Task ClearByTaskIdAsync(string id)
        {
            var entities = await cookieQueryableRepository
                .AsQueryable()
                .Where(x => x.TaskId == id)
                .ToListAsync();
            dbContext.Cookies.RemoveRange(entities);
        }

        public async Task<IReadOnlyCollection<CookieEntity>> GetRangeByTaskAsync(string taskId)
        {
            return await cookieQueryableRepository
                .AsQueryable()
                .Where(x => x.TaskId == taskId)
                .ToListAsync();
        }
    }
}
