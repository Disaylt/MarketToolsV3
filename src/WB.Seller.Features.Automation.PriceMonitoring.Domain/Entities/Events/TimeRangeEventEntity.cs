using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Features.Automation.PriceMonitoring.Domain.Entities.Events
{
    public class TimeRangeEventEntity : EventEntity
    {
        public required DateTime From { get; set; }
        public required DateTime To { get; set; }
    }
}
