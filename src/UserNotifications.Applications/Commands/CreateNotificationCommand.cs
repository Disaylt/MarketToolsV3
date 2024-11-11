using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace UserNotifications.Applications.Commands
{
    public class CreateNotificationCommand : ICommand<Unit>
    {
        public required string UserId { get; set; }
        public required string Message { get; set; }
    }
}
