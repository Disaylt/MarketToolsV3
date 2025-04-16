using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.IntegrationEventLogService.Models
{
    public record IntegrationEventInfoDto(Type Type, object Content)
    {
    }
}
