using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class FakeDataTask : Entity
    {
        public override int Id => throw new NotImplementedException($"Id not implement, use {nameof(TaskId)}");
        public virtual string TaskId { get; private set; } = Guid.NewGuid().ToString();
        public List<TaskDetails> Details { get; set; } = [];
    }
}
