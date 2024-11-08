using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Domain.Entities
{
    public class Notification
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string UserId { get; set; }
        public required string Message { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
    }
}
