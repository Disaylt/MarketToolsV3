using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Applications.Services
{
    public class NotificationFiltersService : INotificationFiltersService
    {
        public NotificationDateFilter CreateDateFilterFromArray(IEnumerable<Notification> notifications, bool wasSorted = false)
        {
            if(wasSorted == false)
            {
                notifications = notifications.OrderByDescending(x => x.Created);
            }

            return new()
            {
                FromDate = notifications
                    .Last()
                    .Created,
                ToDate = notifications
                    .First()
                    .Created
            };
        }
    }
}
