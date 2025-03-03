using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Utilities.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Utilities.Implementation
{
    public class RandomTemplateParser : IRandomTemplateParser
    {
        public JsonRandomTemplateModel Parse(string value)
        {
            string[] data = value
                .Split('@')[1]
                .Split(':');

            return new()
            {
                Type = data[0],
                Min = int.Parse(data[1]),
                Max = int.Parse(data[2])
            };
        }
    }
}
