using IntegrationEvents.Contract;
using MarketToolsV3.IntegrationEventLogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MarketToolsV3.IntegrationEventLogService.Services.Abstract;

namespace MarketToolsV3.IntegrationEventLogService.Services.Implementation
{
    public class IntegrationEventLogsConvertService : IIntegrationEventLogsConvertService
    {
        public IntegrationEventInfoDto Convert(IntegrationEventLogEntry log)
        {
            Type type = IntegrationsStore.FullNameAndTypePairs
                            .GetValueOrDefault(log.Type)
                        ?? throw new NullReferenceException("Integration event not found");

            object content = JsonSerializer.Deserialize(log.Content, type)
                             ?? throw new JsonException($"Bad deserialize {log.Type}");

            return new(type, content);
        }
    }
}
