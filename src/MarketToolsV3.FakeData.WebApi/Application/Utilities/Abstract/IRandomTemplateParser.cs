using MarketToolsV3.FakeData.WebApi.Application.Models;

namespace MarketToolsV3.FakeData.WebApi.Application.Utilities.Abstract
{
    public interface IRandomTemplateParser
    {
        JsonRandomTemplateModel Parse(string value);
    }
}
