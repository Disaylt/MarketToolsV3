using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Database;
using UserNotifications.Infrastructure.Repositories;
using UserNotifications.Infrastructure.SpecificationHandlers;

namespace UserNotifications.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ServiceConfiguration config)
        {
            services.AddSingleton<IMongoClient>(new MongoClient(config.DatabaseConnection));
            services.AddSingleton(x =>
                x.GetRequiredService<IMongoClient>().GetDatabase(config.DatabaseName));
            services.AddSingleton(x =>
                x.GetRequiredService<IMongoDatabase>().GetCollection<Notification>(config.NotificationsCollectionName));

            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
            services.AddScoped<IClientSessionHandleContext, ClientSessionHandleContext>();
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

            services.AddScoped<ISpecificationHandler<UpdateNotificationSpecification>, UpdateNotificationSpecificationHandler>();
            services.AddScoped<IRangeSpecificationHandler<GetRangeNotificationsSpecification, Notification>, RangeNotificationsSpecificationHandler>();

            return services;
        }
    }
}
