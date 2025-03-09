using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Applications.Seed;
using UserNotifications.Domain.Enums;

namespace UserNotifications.Applications.Commands
{
    public class CreateReadNotificationCollectionCommand : ICommand<IReadOnlyCollection<NotificationDto>>
    {
        public string? UserId { get; set; }
        public Category? Category { get; set; }
        public bool? IsRead { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
