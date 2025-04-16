using MarketToolsV3.IntegrationEventLogService.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.IntegrationEventLogService.Services.Implementation;

namespace MarketToolsV3.IntegrationEventLogService
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddIntegrationEventLogServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IIntegrationEventLogsConvertService, IntegrationEventLogsConvertService>();

            return serviceCollection;
        }
    }
}
