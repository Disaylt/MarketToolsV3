using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.QueryObjects;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Extensions;

namespace UserNotifications.Infrastructure.QueryObjectHandlers
{
    internal class SearchNotificationQueryObjectHandler(IRepository<Notification> notificationRepository)
        : IQueryObjectHandler<SearchNotificationQueryObject, Notification>
    {
        public IQueryable<Notification> Create(SearchNotificationQueryObject queryObject)
        {
            return notificationRepository
                .AsQueryable()
                .WhereIf(queryObject.Category != null, x => x.Category == queryObject.Category)
                .WhereIf(queryObject.UserId != null, x => x.UserId == queryObject.UserId)
                .WhereIf(queryObject.IsRead != null, x => x.IsRead == queryObject.IsRead);
        }
    }
}
