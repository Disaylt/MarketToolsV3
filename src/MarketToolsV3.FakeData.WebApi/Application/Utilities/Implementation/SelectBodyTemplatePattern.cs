using MarketToolsV3.FakeData.WebApi.Application.Enums;
using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Utilities.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Utilities.Implementation
{
    public class SelectBodyTemplatePattern : ISelectBodyTemplatePattern
    {
        public SelectBodyTemplateModel Parse(string value)
        {

            string[] data = value.Split('@')[1].Split(':');

            return new SelectBodyTemplateModel
            {
                Tag = data[0],
                Paths = data.Skip(1).ToArray()
            };
        }
    }
}
