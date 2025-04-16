using MarketToolsV3.EventBus.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.EventBus.Services.Implementation;

namespace MarketToolsV3.EventBus
{
    public static class RegistrationServicesExtension
    {
        public static IServiceCollection AddEventBus(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IEventBusService, EventBusService>();

            return serviceCollection;
        }
    }
}
