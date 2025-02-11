namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
