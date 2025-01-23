using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Database;
using UserNotifications.Infrastructure.Utilities;

namespace UserNotifications.Infrastructure.SpecificationHandlers
{
    internal class UpdateNotificationSpecificationHandler(IMongoCollection<Notification> collection,
        IClientSessionHandleContext clientSessionHandleContext)
        : ISpecificationHandler<UpdateNotificationSpecification>
    {
        public async Task HandleAsync(UpdateNotificationSpecification specification)
        {
            var update = Builders<Notification>.Update;
            List<UpdateDefinition<Notification>> updates = new();

            if (specification.Data.IsRead.HasValue)
            {
                updates.Add(update.Set(n => n.IsRead, specification.Data.IsRead.Value));
            }

            if(updates.Count == 0)
            {
                return;
            }

            var mongoFilter = MongoFilterUtility.CreateOrEmpty(specification.Filter);

            await collection.UpdateManyAsync(clientSessionHandleContext.Session, mongoFilter, update.Combine(updates));
        }
    }
}
