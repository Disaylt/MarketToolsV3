using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserNotifications.Applications.QueryObjects;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Queries
{
    public class CountNewNotificationsQueryHandler(
        IQueryObjectHandler<SearchNotificationQueryObject, Notification> queryObjectHandler,
        IExtensionRepository extensionRepository)
        : IRequestHandler<CountNewNotificationsQuery, int>
    {
        public async Task<int> Handle(CountNewNotificationsQuery request, CancellationToken cancellationToken)
        {
            SearchNotificationQueryObject queryObject = CreateQueryObject(request);
            var query = queryObjectHandler.Create(queryObject);

            return await extensionRepository.CountAsync(query, cancellationToken);
        }

        private SearchNotificationQueryObject CreateQueryObject(CountNewNotificationsQuery request)
        {
            return new()
            {
                UserId = request.UserId,
                IsRead = false
            };
        }
    }
}
