using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class ResponseBody : Entity
    {
        public string? Data { get; set; }

        public TaskDetails TaskDetails { get; set; } = null!;
        public int TaskDetailsId { get; set; }

        public List<ValueUseHistory> ValueUseHistories { get; private set; } = [];
    }
}
