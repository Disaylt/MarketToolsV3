using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications.Notification;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Utilities;

namespace UserNotifications.Infrastructure.SpecificationHandlers
{
    internal class GetRangeNotificationsSpecificationHandler(IMongoCollection<Notification> collection)
        : IRangeSpecificationHandler<GetRange, Notification>
    {
        public async Task<IEnumerable<Notification>> HandleAsync(GetRange specification)
        {
            var mongoFilter = MongoFilterUtility.CreateOrEmpty(specification.Filter);

            return await collection
                .Find(mongoFilter)
                .ToListAsync();
        }
    }
}
