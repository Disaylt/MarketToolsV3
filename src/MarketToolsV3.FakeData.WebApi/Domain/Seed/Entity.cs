namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public abstract class Entity
    {
        public virtual int Id { get; protected set; }
        public DateTime Created { get; private set; } = DateTime.UtcNow;
    }
}
