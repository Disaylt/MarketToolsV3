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
        ISpecificationHandler<UpdateIsReadFieldByRangeNotificationSpecification> updateSpecificationHandler,
        IRangeSpecificationHandler<GetRangeForUsersNotificationSpecification, Notification> getRangeSpecificationHandler,
        INotificationMapper<NotificationDto> notificationMapper,
        INotificationFiltersService notificationFiltersService)
        : IRequestHandler<CreateNotificationsListForUserCommand, IReadOnlyCollection<NotificationDto>>
    {
        public async Task<IReadOnlyCollection<NotificationDto>> Handle(CreateNotificationsListForUserCommand request, CancellationToken cancellationToken)
        {
            GetRangeForUsersNotificationSpecification getRangeSpecification = CreateGetRangeSpecification(request);
            IReadOnlyCollection<Notification> notifications = await getRangeSpecificationHandler.HandleAsync(getRangeSpecification);

            if (notifications.Count > 0)
            {
                NotificationDateFilter notificationDateFilter = notificationFiltersService.CreateDateFilterFromArray(notifications, true);
                UpdateIsReadFieldByRangeNotificationSpecification updateSpecification = CreateUpdateReadFieldSpecification(notificationDateFilter, request);
                await updateSpecificationHandler.HandleAsync(updateSpecification);
            }

            return notifications
                .Select(notificationMapper.Map)
                .ToList();
        }

        private static UpdateIsReadFieldByRangeNotificationSpecification CreateUpdateReadFieldSpecification(
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

            return new UpdateIsReadFieldByRangeNotificationSpecification(filterData, entityData);

        }

        private static GetRangeForUsersNotificationSpecification CreateGetRangeSpecification
            (CreateNotificationsListForUserCommand request)
        {
            Specifications.Notifications.GetRangeByDateUserAndLimit.FilterData filterData = new()
            {
                UserId = request.UserId,
                Category = request.Category,
                IsRead = request.IsRead
            };

            GetRangeForUsersNotificationSpecification specification = new(filterData)
            {
                Options =
                {
                    Take = request.Take,
                    Skip = request.Skip
                }
            };

            return specification;
        }
    }
}
