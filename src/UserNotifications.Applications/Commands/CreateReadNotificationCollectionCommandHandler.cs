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
        : IRequestHandler<CreateReadNotificationCollectionCommand, PaginationDto<NotificationDto>>
    {
        public async Task<PaginationDto<NotificationDto>> Handle(CreateReadNotificationCollectionCommand request, CancellationToken cancellationToken)
        {
            SearchNotificationQueryObject queryObject = CreateQueryObject(request);

            var generalQuery = queryObjectHandler
                .Create(queryObject);

            var paginationQuery = generalQuery
                .Skip(request.Skip)
                .Take(request.Take);

            var transferQuery = await notificationToTransferQueryableHandler.HandleAsync(paginationQuery);
            var notifications = await extensionRepository.ToListAsync(transferQuery, cancellationToken);

            await MarkAsReadAsync(notifications);

            return new()
            {
                Items = notifications,
                Total = await extensionRepository.CountAsync(generalQuery, cancellationToken)
            };
        }

        private async Task MarkAsReadAsync(IEnumerable<NotificationDto> notifications)
        {
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
        }

        private SearchNotificationQueryObject CreateQueryObject(CreateReadNotificationCollectionCommand request)
        {
            return new()
            {
                Category = request.Category,
                IsRead = request.IsRead,
                UserId = request.UserId
            };
        }
    }
}
