using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation
{
    public class TaskDetailsEntityService(FakeDataDbContext context)
        : ITaskDetailsEntityService
    {
        private readonly DbSet<TaskDetails> _dbSet = context.Set<TaskDetails>();
        public async Task SetGroupAsSkipAsync(string taskId, int groupId)
        {
            await _dbSet
                .Where(x => x.TaskId == taskId
                            && x.NumGroup == groupId
                            && x.State == TaskDetailsState.AwaitRun)
                .ExecuteUpdateAsync(x=> x
                    .SetProperty(p => p.State, TaskDetailsState.Skip));
        }

        public async Task<TaskDetails?> TakeNextAsync(string taskId)
        {
            return await _dbSet
                .OrderBy(x => x.SortIndex)
                .FirstOrDefaultAsync(x =>
                    x.State == TaskDetailsState.AwaitRun &&
                    x.TaskId == taskId);
        }
    }
}
