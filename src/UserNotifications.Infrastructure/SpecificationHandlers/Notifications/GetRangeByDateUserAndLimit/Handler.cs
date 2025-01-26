using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications.Notifications.GetRangeByDateUserAndLimit;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Utilities;

namespace UserNotifications.Infrastructure.SpecificationHandlers.Notifications.GetRangeByDateUserAndLimit
{
    internal class GetRangeByDateUserAndLimitNotificationSpecificationHandler(IMongoCollection<Notification> collection)
        : IRangeSpecificationHandler<GetRangeByDateUserAndLimitNotificationSpecification, Notification>
    {
        public async Task<IEnumerable<Notification>> HandleAsync(GetRangeByDateUserAndLimitNotificationSpecification specification)
        {
            return await collection
                .Find(n =>
                    n.UserId == specification.Filter.UserId)
                .SortByDescending(x => x.Created)
                .Skip(specification.Options.Skip)
                .Limit(specification.Options.Take)
                .ToListAsync();
        }
    }
}
