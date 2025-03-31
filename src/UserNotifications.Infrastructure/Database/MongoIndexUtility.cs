using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace UserNotifications.Infrastructure.Database
{
    internal static class MongoIndexUtility
    {
        public static async Task<HashSet<string>> GetIndexNames<T>(IMongoCollection<T> collection)
        {
            var asyncCursor = await collection.Indexes.ListAsync();
            var bsonIndexes = await asyncCursor.ToListAsync();

            return bsonIndexes
                .Select(x => x
                                 .Elements
                                 .FirstOrDefault(e =>
                                     e.Name == "name")
                                 .Value
                                 .AsString
                                ?? string.Empty)
                .Distinct()
                .ToHashSet();
        }
    }
}
