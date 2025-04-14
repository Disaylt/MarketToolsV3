using MarketToolsV3.EventLogBus.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.EventLogBus
{
    public static class RegistrationServicesExtension
    {
        public static IServiceCollection AddEventLogBus(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEventLogBus, Services.Implementation.EventLogBus>();

            return serviceCollection;
        }
    }
}
