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
        public int NumberOfExecutions { get; set; }
        public int NumSuccessful { get; set; }
        public int NumFailed { get; set; }
        public int NumberInQueue { get; set; }
        public int TimeoutBeforeRun { get; set; }
        public int NumGroup { get; set; }
        public TaskDetailsState State { get; set; } = TaskDetailsState.AwaitRun;

        public List<ResponseBody> Responses { get; private set; } = [];

        public FakeDataTask Task { get; set; } = null!;
        public string TaskId { get; set; } = null!;
    }
}
