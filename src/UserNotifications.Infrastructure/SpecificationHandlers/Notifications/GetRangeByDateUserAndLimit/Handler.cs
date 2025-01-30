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
        : IRangeSpecificationHandler<GetRangeForUsersNotificationSpecification, Notification>
    {
        public async Task<IReadOnlyCollection<Notification>> HandleAsync(GetRangeForUsersNotificationSpecification specification)
        {
            var filter = specification.Filter;
            return await collection
                .Find(n =>
                    n.UserId == filter.UserId
                    && (filter.IsRead.HasValue == false || n.IsRead == filter.IsRead.Value))
                .SortByDescending(x => x.Created)
                .Skip(specification.Options.Skip)
                .Limit(specification.Options.Take)
                .ToListAsync();
        }
    }
}
