using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Applications.Commands
{
    public class MarkAsReadNotificationsRangeCommand : ICommand<Unit>
    {
        public required string UserId { get; set; }
        public required DateTime FromDate { get; set; }
        public required DateTime ToDate { get; set; }
    }

    public class MarkAsReadNotificationsRangeCommandHandler 
        : IRequestHandler<MarkAsReadNotificationsRangeCommand, Unit>
    {
        public Task<Unit> Handle(MarkAsReadNotificationsRangeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
