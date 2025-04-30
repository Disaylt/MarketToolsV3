using MongoDB.Driver;

namespace MarketToolsV3.PermissionStore.Infrastructure.Database
{
    public class ClientSessionHandleContext(IMongoClient client) : IClientSessionHandleContext, IDisposable, IAsyncDisposable
    {
        public IClientSessionHandle Session { get; } = client.StartSession();

        public void Dispose()
        {
            Session.Dispose();
            GC.SuppressFinalize(this);
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            GC.SuppressFinalize(this);

            return ValueTask.CompletedTask;
        }
    }
}
