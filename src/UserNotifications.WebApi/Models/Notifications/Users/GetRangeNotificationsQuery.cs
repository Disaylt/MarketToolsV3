using System.ComponentModel.DataAnnotations;

namespace UserNotifications.WebApi.Models.Notifications.Users
{
    public class GetRangeNotificationsQuery
    {
        [Range(0, 50, ErrorMessage = "Вне диапазона от 0 до 50.")]
        public int Take { get; set; } = 10;

        [Range(0, int.MaxValue, ErrorMessage = $"Вне диапазона.")]
        public int Skip { get; set; }
    }
}
