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
        private const string UserSearchIndex = "userSearchIndex";
        private const string UserSearchWithIsReadIndex = "userSearchWithIsReadIndex";
        public async Task Build()
        {
            HashSet<string> existsIndexNames = await MongoIndexUtility.GetIndexNames(collection);

            if (existsIndexNames.Contains(UserSearchIndex) == false)
            {
                var userSearchIndex = CreateUserSearchIndex();
                await collection.Indexes.CreateOneAsync(userSearchIndex);
            }

            if (existsIndexNames.Contains(UserSearchWithIsReadIndex) == false)
            {
                var userSearchWithIsReadIndex = CreateUserSearchWithIsReadIndex();
                await collection.Indexes.CreateOneAsync(userSearchWithIsReadIndex);
            }
        }

        private CreateIndexModel<Notification> CreateUserSearchIndex()
        {
            var userSearchIndexKey = Builders<Notification>
                .IndexKeys
                .Ascending(x => x.UserId)
                .Descending(x => x.Created);

            return new CreateIndexModel<Notification>(userSearchIndexKey, new CreateIndexOptions
            {
                Name = UserSearchIndex
            });
        }

        private CreateIndexModel<Notification> CreateUserSearchWithIsReadIndex()
        {
            var userSearchWithIsReadIndexKey = Builders<Notification>
                .IndexKeys
                .Ascending(x => x.UserId)
                .Ascending(x => x.IsRead)
                .Descending(x => x.Created);

            return new CreateIndexModel<Notification>(userSearchWithIsReadIndexKey, new CreateIndexOptions
            {
                Name = UserSearchWithIsReadIndex
            });
        }
    }
}
