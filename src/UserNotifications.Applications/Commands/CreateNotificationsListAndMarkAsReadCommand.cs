using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models.UpdateDetails;
using UserNotifications.Applications.Specifications;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Commands
{
    public class CreateNotificationsListAndMarkAsReadCommand : ICommand<Unit>
    {
        public required string UserId { get; set; }
        public required DateTime FromDate { get; set; }
        public required DateTime ToDate { get; set; }
    }

    public class CreateNotificationsListAndMarkAsReadCommandHandler(ISpecificationHandler<UpdateReadStatusByFilterNotificationSpecification> updateSpecificationHandler)
        : IRequestHandler<CreateNotificationsListAndMarkAsReadCommand, Unit>
    {
        public async Task<Unit> Handle(CreateNotificationsListAndMarkAsReadCommand request, CancellationToken cancellationToken)
        {
            UpdateReadStatusByFilterNotificationSpecification specification = new UpdateReadStatusByFilterNotificationSpecification();
            specification.Data.IsRead = true;
            specification.Filter = (notification) 
                => notification.UserId == request.UserId
                && notification.Created >= request.FromDate
                && notification.Created <= request.ToDate;

            await updateSpecificationHandler.HandleAsync(specification);

            return Unit.Value;
        }
    }
}
