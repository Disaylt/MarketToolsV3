using MarketToolsV3.IntegrationEventLogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.IntegrationEventLogService.Services.Abstract
{
    public interface IIntegrationEventLogsConvertService
    {
        IntegrationEventInfoDto Convert(IntegrationEventLogEntry log);
    }
}
