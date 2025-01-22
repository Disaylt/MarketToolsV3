using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;

namespace UserNotifications.Applications.Mappers
{
    public interface INotificationMapper<out TResult> where TResult : IMap
    {
        TResult Map(NotificationDto notification);
    }
}
