using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Domain.UpdateDetails
{
    public interface INotificationUpdateDetails : IUpdateDetails<Notification>
    {
        bool? IsRead { get; set; }
    }
}
