using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Enums;

namespace UserNotifications.Applications.UpdateModels
{
    public record NotificationUpdateModel : BaseUpdateModel
    {
        public string? Message { get; init; }
        public string? Title { get; init; }
        public bool? IsRead { get; init; }
        public Category? Category { get; init; }
    }
}
