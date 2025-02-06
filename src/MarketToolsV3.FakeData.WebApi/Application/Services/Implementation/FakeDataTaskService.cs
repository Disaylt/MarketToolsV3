using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskService(IFakeDataTaskMapService fakeDataTaskMapService,
        IFakeDataTaskEntityService fakeDataTaskEntityService)
        : IFakeDataTaskService
    {
        public async Task<FakeDataTaskDto> CreateAsync(NewFakeDataTaskDto task)
        {
            FakeDataTask entity = fakeDataTaskMapService.Map(task);
            await fakeDataTaskEntityService.AddAsync(entity);

            return CreateResult(entity);
        }

        private FakeDataTaskDto CreateResult(FakeDataTask entity)
        {
            return new FakeDataTaskDto
            {
                Id = entity.TaskId,
                State = entity.State
            };
        }
    }
}
