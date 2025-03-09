using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserNotifications.Applications.Seed;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Enums;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Commands
{
    public class CreateNotificationCommand : ICommand<Unit>
    {
        public required string UserId { get; set; }
        public required string Message { get; set; }
        public string? Title { get; set; }
        public Category Category { get; set; }
    }
}
