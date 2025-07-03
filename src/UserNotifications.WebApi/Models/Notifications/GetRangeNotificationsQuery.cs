using System.ComponentModel.DataAnnotations;

namespace UserNotifications.WebApi.Models.Notifications
{
    public class GetRangeNotificationsQuery
    {
        public string? Category { get; set; }
        public bool? IsRead { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
