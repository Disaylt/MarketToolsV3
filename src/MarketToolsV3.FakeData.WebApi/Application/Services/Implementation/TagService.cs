using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class TagService : ITagService
    {
        public TagTemplateModel Parse(string tagData)
        {
            string[] splitData = tagData.Split('[', ']');

            return new TagTemplateModel
            {
                Name = splitData[0],
                Index = int.Parse(splitData[1])
            };
        }
    }
}
