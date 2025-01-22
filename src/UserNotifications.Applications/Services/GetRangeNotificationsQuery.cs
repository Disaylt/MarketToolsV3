using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Applications.Specifications;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Services
{
    public class GetRangeNotificationsQuery : IRequest<IReadOnlyCollection<NotificationDto>>
    {
        public GetRangeNotificationsSpecification Data { get; set; } = new();
    }

    public class GetRangeNotificationsQueryHandler(IRangeSpecificationHandler<GetRangeNotificationsSpecification, Notification> rangeSpecificationHandler)
        : IRequestHandler<GetRangeNotificationsQuery, IReadOnlyCollection<NotificationDto>>
    {
        public async Task<IReadOnlyCollection<NotificationDto>> Handle(GetRangeNotificationsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Notification> entityNotifications = await rangeSpecificationHandler.HandleAsync(request.Data);

            return entityNotifications
                .Select(n => new NotificationDto
                {
                    Created = n.Created,
                    IsRead = n.IsRead,
                    Message = n.Message,
                    UserId = n.UserId
                })
                .ToList();
        }
    }
}
