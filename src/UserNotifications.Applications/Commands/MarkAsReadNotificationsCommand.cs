using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Commands
{
    public class MarkAsReadNotificationsCommand : ICommand<Unit>
    {
        public required string UserId { get; set; }
        public required DateTime FromDate { get; set; }
        public required DateTime ToDate { get; set; }
    }

    public class MarkAsReadNotificationsRangeCommandHandler
        : IRequestHandler<MarkAsReadNotificationsCommand, Unit>
    {
        public Task<Unit> Handle(MarkAsReadNotificationsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
