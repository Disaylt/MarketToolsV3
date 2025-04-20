using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace UserNotifications.Applications.Queries
{
    public class CountNewNotificationsQuery : IRequest<int>
    {
        public required string UserId { get; set; }
    }
}
