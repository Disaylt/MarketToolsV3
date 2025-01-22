using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Infrastructure.SpecificationHandlers
{
    internal class RangeNotificationsSpecificationHandler(IMongoCollection<Notification> collection)
        : IRangeSpecificationHandler<GetRangeNotificationsSpecification, Notification>
    {
        public async Task<IEnumerable<Notification>> HandleAsync(GetRangeNotificationsSpecification specification)
        {
            var filter = Builders<Notification>.Filter;
            List<FilterDefinition<Notification>> filters = new();

            if (string.IsNullOrEmpty(specification.UserId) == false)
            {
                filters.Add(filter.Eq(x => x.UserId, specification.UserId));
            }

            if (specification.FromDate.HasValue)
            {
                filters.Add(filter.Gte(x => x.Created, specification.FromDate.Value));
            }

            if (specification.ToDate.HasValue)
            {
                filters.Add(filter.Lte(x => x.Created, specification.ToDate.Value));
            }

            var combineFilter = filters.Count > 0
                ? filter.And(filters)
                : filter.Empty;

            return await collection
                .Find(combineFilter)
                .ToListAsync();
        }
    }
}
