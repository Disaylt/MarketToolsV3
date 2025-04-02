using MarketToolsV3.FakeData.WebApi.Application.Features.TaskDetails.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Features.TaskDetails.Services.Implementation
{
    public class TaskDetailsEntityService(FakeDataDbContext context)
        : ITaskDetailsEntityService
    {
        private readonly DbSet<TaskDetailsEntity> _dbSet = context.Set<TaskDetailsEntity>();
        public async Task SetGroupAsSkipAsync(string taskId, int groupId)
        {
            await _dbSet
                .Where(x => x.TaskId == taskId
                            && x.NumGroup == groupId
                            && x.State == TaskDetailsState.AwaitRun)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.State, TaskDetailsState.Skip));
        }

        public async Task<TaskDetailsEntity?> TakeNextAsync(string taskId)
        {
            return await _dbSet
                .OrderBy(x => x.SortIndex)
                .FirstOrDefaultAsync(x =>
                    x.State == TaskDetailsState.AwaitRun &&
                    x.TaskId == taskId);
        }
    }
}
