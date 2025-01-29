using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Mappers.Abstract;
using UserNotifications.Applications.Models;
using UserNotifications.Applications.Services.Abstract;
using UserNotifications.Applications.Specifications.Notifications.GetRangeByDateUserAndLimit;
using UserNotifications.Applications.Specifications.Notifications.UpdateIsReadByRange;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Commands
{
    public class CreateNotificationsListForUserCommandHandler(
        ISpecificationHandler<UpdateIsReadByRangeFilterNotificationSpecififcation> updateSpecificationHandler,
        IRangeSpecificationHandler<GetRangeByDateUserAndLimitNotificationSpecification, Notification> getRangeSpecificationHandler,
        INotificationMapper<NotificationDto> notificationMapper,
        INotificationFiltersService notificationFiltersService)
        : IRequestHandler<CreateNotificationsListForUserCommand, IReadOnlyCollection<NotificationDto>>
    {
        public async Task<IReadOnlyCollection<NotificationDto>> Handle(CreateNotificationsListForUserCommand request, CancellationToken cancellationToken)
        {
            GetRangeByDateUserAndLimitNotificationSpecification getRangeSpecification = CreateGetRangeSpecification(request);
            IReadOnlyCollection<Notification> notifications = await getRangeSpecificationHandler.HandleAsync(getRangeSpecification);

            if (notifications.Count > 0)
            {
                NotificationDateFilter notificationDateFilter = notificationFiltersService.CreateDateFilterFromArray(notifications, true);
                UpdateIsReadByRangeFilterNotificationSpecififcation updateSpecification = CreateUpdateReadFieldSpecification(notificationDateFilter, request);
                await updateSpecificationHandler.HandleAsync(updateSpecification);
            }

            return notifications
                .Select(notificationMapper.Map)
                .ToList();
        }

        private UpdateIsReadByRangeFilterNotificationSpecififcation CreateUpdateReadFieldSpecification(
            NotificationDateFilter notificationDateFilter,
            CreateNotificationsListForUserCommand request)
        {
            Specifications.Notifications.UpdateIsReadByRange.FilterData filterData = new()
            {
                FromDate = notificationDateFilter.FromDate,
                ToDate = notificationDateFilter.ToDate,
                UserId = request.UserId
            };
            Specifications.Notifications.UpdateIsReadByRange.EntityData entityData = new()
            {
                IsRead = true
            };

            return new UpdateIsReadByRangeFilterNotificationSpecififcation(filterData, entityData);

        }

        private GetRangeByDateUserAndLimitNotificationSpecification CreateGetRangeSpecification
            (CreateNotificationsListForUserCommand request)
        {
            Specifications.Notifications.GetRangeByDateUserAndLimit.FilterData filterData = new()
            {
                UserId = request.UserId,
            };

            GetRangeByDateUserAndLimitNotificationSpecification specification = new(filterData);
            specification.Options.Take = request.Take;
            specification.Options.Skip = request.Skip;

            return specification;
        }
    }
}
