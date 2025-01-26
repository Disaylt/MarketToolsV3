using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Mappers;
using UserNotifications.Applications.Models;
using UserNotifications.Applications.Specifications.Notification;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Queries
{
    public class GetRangeNotificationsQuery : IRequest<IReadOnlyCollection<NotificationDto>>
    {
        public GetRange Data { get; set; } = new();
    }

    public class GetRangeNotificationsQueryHandler(INotificationMapper<NotificationDto> notificationMapper,
        IRangeSpecificationHandler<GetRange, Notification> rangeSpecificationHandler)
        : IRequestHandler<GetRangeNotificationsQuery, IReadOnlyCollection<NotificationDto>>
    {
        public async Task<IReadOnlyCollection<NotificationDto>> Handle(GetRangeNotificationsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Notification> entityNotifications = await rangeSpecificationHandler.HandleAsync(request.Data);

            return entityNotifications
                .Select(notificationMapper.Map)
                .ToList();
        }
    }
}
