using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class TaskDetails : Entity
    {
        public required string Path { get; set; }
        public required string Method { get; set; }
        public string? JsonBody { get; set; }
        public TaskAttemptOption TaskAttemptOption { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumSuccess { get; set; }
        public int NumFailed { get; set; }

        public List<ResponseBody> Responses { get; set; } = new();

        public FakeDataTask Task { get; set; } = null!;
        public string TaskId { get; set; } = null!;
    }
}
