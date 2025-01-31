using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Infrastructure.Database.MongoCollectionBuilders
{
    public class NotificationCollectionBuilder(IMongoCollection<Notification> collection)
    {
        private readonly LinkedList<CreateIndexModel<Notification>> _indexes = [];

        public async Task Build()
        {
            HashSet<string> existsIndexNames = await MongoIndexUtility.GetIndexNames(collection);

            AddOrSkipUserSearchIndex(existsIndexNames);
            AddOrSkipUserSearchWithIsReadIndex(existsIndexNames);

            await collection.Indexes.CreateManyAsync(_indexes);
        }

        private void AddOrSkipUserSearchIndex(HashSet<string> existsIndexNames)
        {
            CreateIndexOptions indexOptions = new()
            {
                Name = "userSearchIndex"
            };

            if (existsIndexNames.Contains(indexOptions.Name))
            {
                return;
            }

            var userSearchIndexKey = Builders<Notification>
                .IndexKeys
                .Ascending(x => x.UserId)
                .Descending(x => x.Created);

            CreateIndexModel<Notification> index = new(userSearchIndexKey, indexOptions);

            _indexes.AddLast(index);
        }

        private void AddOrSkipUserSearchWithIsReadIndex(HashSet<string> existsIndexNames)
        {
            CreateIndexOptions indexOptions = new()
            {
                Name = "userSearchWithIsReadIndex"
            };

            if (existsIndexNames.Contains(indexOptions.Name))
            {
                return;
            }

            var userSearchWithIsReadIndexKey = Builders<Notification>
                .IndexKeys
                .Ascending(x => x.UserId)
                .Ascending(x => x.IsRead)
                .Descending(x => x.Created);

            CreateIndexModel<Notification> index = new(userSearchWithIsReadIndexKey, indexOptions);

            _indexes.AddLast(index);
        }
    }
}
