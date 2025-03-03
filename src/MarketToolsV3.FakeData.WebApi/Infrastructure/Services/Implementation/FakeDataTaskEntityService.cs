using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation
{
    public class FakeDataTaskEntityService(FakeDataDbContext dbContext)
        : IFakeDataTaskEntityService
    {
        public async Task AddAsync(FakeDataTaskEntity entity)
        {
            await dbContext.Tasks.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<FakeDataTaskEntity?> FindAsync(string id)
        {
            return await dbContext
                .Tasks
                .FindAsync(id);
        }
    }
}
