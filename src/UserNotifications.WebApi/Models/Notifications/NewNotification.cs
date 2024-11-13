using System.ComponentModel.DataAnnotations;

namespace UserNotifications.WebApi.Models.Notifications
{
    public class NewNotification
    {
        [Required]
        [MaxLength(100)]
        public required string UserId { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string Message { get; set; }
    }
}