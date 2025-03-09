using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserNotifications.Applications.Mappers.Abstract;
using UserNotifications.Applications.Models;
using UserNotifications.Applications.QueryObjects;
using UserNotifications.Applications.Services.Abstract;
using UserNotifications.Applications.UpdateModels;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Extensions;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Commands
{
    public class CreateReadNotificationCollectionCommandHandler(
        IQueryObjectHandler<SearchNotificationQueryObject, Notification> queryObjectHandler,
        IUpdateEntityService<NotificationUpdateModel> notificationUpdateEntityService,
        IQueryableHandler<Notification, NotificationDto> notificationToTransferQueryableHandler,
        IExtensionRepository extensionRepository)
        : IRequestHandler<CreateReadNotificationCollectionCommand, IReadOnlyCollection<NotificationDto>>
    {
        public async Task<IReadOnlyCollection<NotificationDto>> Handle(CreateReadNotificationCollectionCommand request, CancellationToken cancellationToken)
        {
            SearchNotificationQueryObject queryObject = new()
            {
                Category = request.Category,
                IsRead = request.IsRead,
                UserId = request.UserId
            };

            var query = queryObjectHandler
                .Create(queryObject)
                .Skip(request.Skip)
                .Take(request.Take);

            var transferQuery = await notificationToTransferQueryableHandler.HandleAsync(query);
            var notifications = await extensionRepository.ToListAsync(transferQuery);

            var notificationsNotReadIds = notifications
                .Where(x => x.IsRead == false)
                .Select(x => x.Id)
                .ToList();

            NotificationUpdateModel updateModel = new()
            {
                IsRead = true,
                Filter = x => notificationsNotReadIds.Contains(x.Id)
            };

            await notificationUpdateEntityService.UpdateManyAsync(updateModel);

            return notifications;
        }
    }
}
