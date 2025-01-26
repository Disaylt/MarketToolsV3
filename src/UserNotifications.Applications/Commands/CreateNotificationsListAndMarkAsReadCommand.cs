using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Applications.Specifications.Notifications.GetRangeByDateUserAndLimit;
using UserNotifications.Applications.Specifications.Notifications.UpdateIsReadByRange;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Commands
{
    public class CreateNotificationsListAndMarkAsReadCommand : ICommand<IReadOnlyCollection<NotificationDto>>
    {
        public required string UserId { get; set; }
        public required DateTime FromDate { get; set; }
        public required DateTime ToDate { get; set; }
    }

    public class CreateNotificationsListAndMarkAsReadCommandHandler(
        ISpecificationHandler<UpdateIsReadByRangeFilterNotificationSpecififcation> updateSpecificationHandler,
        IRangeSpecificationHandler<GetRangeByDateUserAndLimitNotificationSpecification, Notification> getRangeSpecificationHandler)
        : IRequestHandler<CreateNotificationsListAndMarkAsReadCommand, IReadOnlyCollection<NotificationDto>>
    {
        public async Task<IReadOnlyCollection<NotificationDto>> Handle(CreateNotificationsListAndMarkAsReadCommand request, CancellationToken cancellationToken)
        {
            throw new Exception();
        }
    }
}
