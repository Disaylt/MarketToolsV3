using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class TaskEntity : Entity
    {
        public override int Id => throw new NotImplementedException($"Id not implement, use {nameof(TaskId)}");
        public virtual string TaskId { get; private set; } = Guid.NewGuid().ToString();
        public TaskState State { get; set; } = TaskState.AwaitRun;
        public List<TaskDetailsEntity> Details { get; set; } = [];
        public List<CookieEntity> Cookies { get; set; } = [];
    }
}
