using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation
{
    public class FakeDataDetailsEntityService(FakeDataDbContext context)
        : IFakeDataDetailsEntityService
    {
        public async Task<TaskDetails?> TakeNextAsync()
        {
            return await context
                .Set<TaskDetails>()
                .OrderBy(x => x.SortIndex)
                .FirstOrDefaultAsync(x =>
                    x.State == TaskDetailsState.AwaitRun);
        }
    }
}
