using MarketToolsV3.FakeData.WebApi.Application.Models;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface ITagService
    {
        TagTemplateModel Parse(string tagData);
    }
}
