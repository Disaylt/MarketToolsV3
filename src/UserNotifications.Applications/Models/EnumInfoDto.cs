using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Applications.Models
{
    public record EnumInfoDto
    {
        public int Id { get; init; }
        public string Description { get; init; } = "Неизвестно";
    }
}
