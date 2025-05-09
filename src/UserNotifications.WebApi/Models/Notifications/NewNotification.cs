﻿using System.ComponentModel.DataAnnotations;
using UserNotifications.Domain.Enums;

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

        public string? Title { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}