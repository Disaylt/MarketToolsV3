using System.Text.Json;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class ResponseValue : Entity
    {
        public required string Path { get; set; }
        public required JsonValueKind Kind { get; set; }
        public required string Value { get; set; }

        public TaskDetails TaskDetails { get; set; } = null!;
        public int TaskDetailsId { get; set; }
    }
}
