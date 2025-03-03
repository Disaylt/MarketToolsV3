using System.Linq.Expressions;
using MarketToolsV3.FakeData.WebApi.Application.Features.ResponseBody.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.ResponseBody.Utilities.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Features.ResponseBody.Services.Implementation
{
    public class ResponseBodyTagService(
        IQueryableRepository<ResponseBodyEntity> responseBodyQueryableRepository,
        ITagIndexUtility tagIndexUtility)
        : IResponseBodyTagService
    {
        public async Task<ResponseBodyEntity> SelectByTemplateAsync(string taskId, TagTemplateModel template)
        {
            var searchCondition = CreateTemplateSearchCondition(template.Name, taskId);

            int totalResponses = await responseBodyQueryableRepository
                .AsQueryable()
                .CountAsync(searchCondition);

            int index = tagIndexUtility.GetSkipQuantityByTotalResponse(totalResponses, template.Index);

            return await responseBodyQueryableRepository
                .AsQueryable()
                .OrderBy(x => x.Id)
                .Skip(index)
                .FirstAsync(searchCondition);
        }

        private Expression<Func<ResponseBodyEntity, bool>> CreateTemplateSearchCondition(string tag, string taskId)
        {
            return x => x.TaskDetail.TaskId == taskId &&
                        x.TaskDetail.Tag == tag;
        }
    }
}
