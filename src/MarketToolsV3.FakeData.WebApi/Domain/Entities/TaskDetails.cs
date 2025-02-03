using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class TaskDetails : Entity
    {
        public required string Path { get; set; }
        public string? Tag { get; set; }
        public required string Method { get; set; }
        public string? JsonBody { get; set; }
        public TaskCompleteCondition TaskCompleteCondition { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumSuccessful { get; set; }
        public int NumFailed { get; set; }
        public int NumberInQueue { get; set; }
        public int TimeoutBeforeRun { get; set; }
        public bool MoveOnToNextTaskIfNotCompleted { get; set; }

        public List<ResponseBody> Responses { get; private set; } = [];

        public FakeDataTask Task { get; set; } = null!;
        public string TaskId { get; set; } = null!;
    }
}
