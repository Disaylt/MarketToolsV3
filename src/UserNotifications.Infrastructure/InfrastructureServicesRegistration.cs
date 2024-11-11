using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Infrastructure.Database;

namespace UserNotifications.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IClientSessionHandleContext, ClientSessionHandleContext>();
            services.AddSingleton<IDatabaseClient<IMongoClient>, DatabaseClient>();

            return services;
        }
    }
}
