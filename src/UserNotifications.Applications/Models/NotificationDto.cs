using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Models
{
    public record NotificationDto : IMap
    {
        public required string UserId { get; init; }
        public required string Message { get; init; }
        public DateTime Created { get; init; }
        public bool IsRead { get; init; }
    }
}
