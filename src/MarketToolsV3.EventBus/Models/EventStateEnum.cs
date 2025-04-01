using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.EventBus.Models
{
    public enum EventStateEnum
    {
        NotPublished,
        InProcess,
        Complete,
        Error
    }
}
