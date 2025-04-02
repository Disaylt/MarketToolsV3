
using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.ResponseBody.Services.Abstract
{
    public interface IResponseBodyTagService
    {
        Task<ResponseBodyEntity> SelectByTemplateAsync(string taskId, TagTemplateModel template);
    }
}
