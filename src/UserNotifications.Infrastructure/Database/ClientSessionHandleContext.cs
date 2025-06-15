using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Infrastructure.Database
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
