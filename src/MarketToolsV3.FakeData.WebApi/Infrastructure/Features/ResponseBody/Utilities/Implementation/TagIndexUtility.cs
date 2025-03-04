using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.ResponseBody.Utilities.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Features.ResponseBody.Utilities.Implementation
{
    public class TagIndexUtility : ITagIndexUtility
    {
        public int GetSkipQuantityByTotalResponse(int total, int index)
        {
            if (index >= 0 && index < total)
            {
                return index;
            }

            if (index == -1)
            {
                return Random.Shared.Next(total);
            }

            throw new ArgumentException("Cannot use index");
        }
    }
}
