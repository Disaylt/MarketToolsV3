using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Database;
using UserNotifications.Infrastructure.Repositories;

namespace UserNotifications.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfigurationSection serviceSection)
        {
            ServiceConfiguration config = serviceSection.Get<ServiceConfiguration>()
                                          ?? throw new NullReferenceException("Users config is empty");
            services.AddSingleton<IMongoClient>(new MongoClient(config.DatabaseConnection));
            services.AddSingleton<IMongoDatabase>(x =>
                x.GetRequiredService<IMongoClient>().GetDatabase(config.DatabaseName));
            services.AddSingleton<IMongoCollection<Notification>>(x =>
                x.GetRequiredService<IMongoDatabase>().GetCollection<Notification>(config.NotificationsCollectionName));

            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
            services.AddScoped<IClientSessionHandleContext, ClientSessionHandleContext>();
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

            return services;
        }
    }
}
