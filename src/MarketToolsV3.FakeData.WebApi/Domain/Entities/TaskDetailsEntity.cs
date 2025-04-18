﻿using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class TaskDetailsEntity : Entity
    {
        public required string Path { get; set; }
        public string? Tag { get; set; }
        public required string Method { get; set; }
        public string? JsonBody { get; set; }
        public TaskEndCondition TaskEndCondition { get; set; }
        public TaskCompleteCondition TaskCompleteCondition { get; set; }
        public int NumberOfExecutions { get; set; }
        public int NumSuccessful { get; set; }
        public int NumFailed { get; set; }
        public int SortIndex { get; set; }
        public int TimeoutBeforeRun { get; set; }
        public int? NumGroup { get; set; }
        public TaskDetailsState State { get; set; } = TaskDetailsState.AwaitRun;
        public List<ResponseBodyEntity> Responses { get; private set; } = [];

        public TaskEntity TaskEntity { get; set; } = null!;
        public string TaskId { get; set; } = null!;
    }
}
