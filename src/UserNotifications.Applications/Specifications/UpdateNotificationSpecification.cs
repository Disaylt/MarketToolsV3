using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Specifications
{
    public class UpdateNotificationSpecification : BaseSpecification
    {
        public bool? IsRead { get; set; }
    }
}
