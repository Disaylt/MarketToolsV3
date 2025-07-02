using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Services.Abstract;
using UserNotifications.Applications.UpdateModels;
using UserNotifications.Domain.Entities;
using UserNotifications.Infrastructure.Database;
using UserNotifications.Infrastructure.Utilities;

namespace UserNotifications.Infrastructure.Services.Implementation
{
    public class NotificationUpdateEntityService(IMongoCollection<Notification> collection,
        IClientSessionHandleContext clientSessionHandleContext)
        : IUpdateEntityService<NotificationUpdateModel>
    {
        private readonly IClientSessionHandle _clientSessionHandle = clientSessionHandleContext.Session;

        public async Task UpdateManyAsync(NotificationUpdateModel data)
        {
            var filter = data.Filter is null ? 
                Builders<Notification>.Filter.Empty
                : Builders<Notification>.Filter.Where(data.Filter);

            var updateDefinitions = new UpdateDefinitionContainer<Notification>()
                .AddIfTrue(data.Category != null, x => x.Category, data.Category!)
                .AddIfTrue(data.IsRead.HasValue, x => x.IsRead, data.IsRead!)
                .AddIfTrue(data.Message != null, x => x.Message, data.Message)
                .AddIfTrue(data.Title != null, x => x.Title, data.Title)
                .Collection;

            if (updateDefinitions.Count > 0)
            {
                var combineUpdates = Builders<Notification>.Update.Combine(updateDefinitions);
                await collection.UpdateManyAsync(_clientSessionHandle, filter, combineUpdates);
            }
        }
    }
}
