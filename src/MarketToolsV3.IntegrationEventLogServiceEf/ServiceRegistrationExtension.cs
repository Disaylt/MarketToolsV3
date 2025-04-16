using MarketToolsV3.IntegrationEventLogService.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.IntegrationEventLogServiceEf
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddIntegrationEventLogServiceEf<TContext>(this IServiceCollection serviceCollection) where TContext : DbContext
        {
            serviceCollection.AddScoped<IIntegrationEventLogService, IntegrationEventLogService<TContext>>();

            return serviceCollection;
        }
    }
}
