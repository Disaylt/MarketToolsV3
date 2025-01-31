using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Database.MongoCollectionBuilders;

namespace UserNotifications.Infrastructure.Database
{
    public class MongoDatabaseBuilder : IDisposable, IAsyncDisposable
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<Notification> _notificationsCollection;

        public MongoDatabaseBuilder(ServiceConfiguration config)
        {
            _mongoClient = new MongoClient(config.DatabaseConnection);
            var database = _mongoClient.GetDatabase(config.DatabaseName);

            _notificationsCollection = database.GetCollection<Notification>(config.NotificationsCollectionName);
        }

        public async Task Build()
        {
            await new NotificationCollectionBuilder(_notificationsCollection).Build();
        }

        public void Dispose()
        {
            _mongoClient.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            Dispose();

            return ValueTask.CompletedTask;
        }
    }
}
