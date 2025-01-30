using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Mappers.Abstract;
using UserNotifications.Applications.Models;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Extensions;

namespace UserNotifications.Applications.Mappers.Implementation
{
    internal class NotificationDtoMapper : INotificationMapper<NotificationDto>
    {
        public NotificationDto Map(Notification notification)
        {
            return new NotificationDto
            {
                Created = notification.Created,
                IsRead = notification.IsRead,
                Message = notification.Message,
                UserId = notification.UserId,
                Title = notification.Title,
                Category = notification.Category,
                CategoryView = notification.Category?.GetDescription()
            };
        }
    }
}
