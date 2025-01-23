using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Domain.UpdeteDetails
{
    public interface INotificationUpdateDetails : IUpdateDetails
    {
        bool? IsRead { get; set; }
    }
}
