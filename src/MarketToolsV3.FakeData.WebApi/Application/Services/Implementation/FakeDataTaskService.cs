using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskService(ITaskMapService taskMapService,
        ITaskEntityService taskEntityService,
        IRepository<TaskEntity> taskRepository,
        IUnitOfWork unitOfWork)
        : ITaskService
    {
        public async Task<TaskDto> CreateAsync(IReadOnlyCollection<NewTaskDetailsDto> tasks)
        {
            TaskEntity entity = taskMapService.Map(tasks);
            await taskEntityService.AddAsync(entity);

            return CreateResult(entity);
        }

        public async Task SetState(string id, TaskState state)
        {
            TaskEntity task = await taskRepository.FindRequiredAsync(id);
            task.State = state;

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<TaskDto>> GetAllAsync()
        {
            var query = taskRepository
                .AsQueryable()
                .Select(x => new TaskDto
                {
                    Id = x.TaskId,
                    State = x.State
                });

            return await taskRepository.ToListAsync(query);
        }

        private static TaskDto CreateResult(TaskEntity entity)
        {
            return new TaskDto
            {
                Id = entity.TaskId,
                State = entity.State
            };
        }
    }
}
