using MarketToolsV3.FakeData.WebApi.Application.Models;

namespace MarketToolsV3.FakeData.WebApi.Application.Utilities.Abstract
{
    public interface ISelectBodyTemplatePattern
    {
        SelectBodyTemplateModel Parse(string value);
    }
}
