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
using UserNotifications.Applications.Specifications.Notifications;
using UserNotifications.Applications.Mappers;
using UserNotifications.Applications.Services.Abstract;
using UserNotifications.Applications.Seed;

namespace UserNotifications.Applications.Commands
{
    public class CreateNotificationsListForUserCommand : ICommand<IReadOnlyCollection<NotificationDto>>
    {
        public required string UserId { get; set; }
        public int Take { get; set; } = 20;
        public int Skip { get; set; }
    }
}
