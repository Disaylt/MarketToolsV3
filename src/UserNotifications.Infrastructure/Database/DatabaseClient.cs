using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Infrastructure.Database
{
    public class DatabaseClient(string connectionString, string database)
        : IDatabaseClient<IMongoClient>
    {
        public IMongoClient Client { get; } = new MongoClient(connectionString);
    }
}
