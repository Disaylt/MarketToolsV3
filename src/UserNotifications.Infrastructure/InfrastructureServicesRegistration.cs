using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Database;
using UserNotifications.Infrastructure.Repositories;

namespace UserNotifications.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseClient<IMongoClient>, DatabaseClient>();

            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
            services.AddScoped<IClientSessionHandleContext, ClientSessionHandleContext>();
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

            return services;
        }
    }
}
