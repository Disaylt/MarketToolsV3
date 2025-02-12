using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Database
{
    public class UnitOfWork(FakeDataDbContext fakeDataDbContext)
        : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        public async Task BeginTransactionAsync()
        {
            await fakeDataDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await fakeDataDbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await fakeDataDbContext.Database.RollbackTransactionAsync();
        }

        public void Dispose()
        {
            DisposeAsync()
                .AsTask()
                .Wait();
        }

        public async ValueTask DisposeAsync()
        {
            if (fakeDataDbContext.Database.CurrentTransaction != null)
            {
                await fakeDataDbContext.Database.RollbackTransactionAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await fakeDataDbContext.SaveChangesAsync();
        }
    }
}
