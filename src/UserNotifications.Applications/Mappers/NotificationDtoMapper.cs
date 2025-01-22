using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;

namespace UserNotifications.Applications.Mappers
{
    internal class NotificationDtoMapper : INotificationMapper<NotificationDto>
    {
        public NotificationDto Map(NotificationDto notification)
        {
            return new NotificationDto
            {
                Created = notification.Created,
                IsRead = notification.IsRead,
                Message = notification.Message,
                UserId = notification.UserId
            };
        }
    }
}
