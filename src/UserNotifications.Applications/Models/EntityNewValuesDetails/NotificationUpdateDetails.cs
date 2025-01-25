using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Domain.UpdateDetails;

namespace UserNotifications.Applications.Models.UpdateDetails
{
    public class NotificationUpdateDetails : INotificationUpdateDetails
    {
        public bool? IsRead { get; set; }
    }
}
