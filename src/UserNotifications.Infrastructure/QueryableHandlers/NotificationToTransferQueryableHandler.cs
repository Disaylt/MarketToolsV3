using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Extensions;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Infrastructure.QueryableHandlers
{
    public class NotificationToTransferQueryableHandler : IQueryableHandler<Notification, NotificationDto>
    {
        public Task<IQueryable<NotificationDto>> HandleAsync(IQueryable<Notification> query)
        {
            var result = query
                .Select(notification => new NotificationDto
                {
                    Id = notification.Id,
                    Created = notification.Created,
                    IsRead = notification.IsRead,
                    Message = notification.Message,
                    UserId = notification.UserId,
                    Title = notification.Title,
                    Category = notification.Category
                });

            return Task.FromResult(result);
        }
    }
}
