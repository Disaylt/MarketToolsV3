using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Applications.Services
{
    public interface INotificationFiltersService
    {
        NotificationDateFilter CreateDateFilterFromArray(IEnumerable<Notification> notifications, bool wasSorted = false);
    }
}
