using System.ComponentModel.DataAnnotations;
using UserNotifications.Domain.Enums;

namespace UserNotifications.WebApi.Models.Notifications
{
    public class GetRangeNotificationsQuery
    {
        public Category? Category { get; set; }
        public bool? IsRead { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
