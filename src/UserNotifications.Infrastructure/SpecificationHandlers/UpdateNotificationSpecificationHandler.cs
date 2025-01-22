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

            var filter = Builders<Notification>.Filter;
            List<FilterDefinition< Notification>> filters = new();

            if (string.IsNullOrEmpty(specification.Filter.UserId) == false)
            {
                filters.Add(filter.Eq(x => x.UserId, specification.Filter.UserId));
            }

            if (specification.Filter.FromDate.HasValue)
            {
                filters.Add(filter.Gte(x => x.Created, specification.Filter.FromDate.Value));
            }

            if (specification.Filter.ToDate.HasValue)
            {
                filters.Add(filter.Lte(x => x.Created, specification.Filter.ToDate.Value));
            }

            var combineFilter = filters.Count > 0 
                ? filter.And(filters)
                : filter.Empty;

            await collection.UpdateManyAsync(clientSessionHandleContext.Session, combineFilter, update.Combine(updates));
        }
    }
}
