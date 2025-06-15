using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Features.Automation.PriceMonitoring.Domain.Seed;

namespace WB.Seller.Features.Automation.PriceMonitoring.Domain.Entities.Events
{
    public class EventEntity : Entity
    {
        public int Priority { get; set; }
    }
}
