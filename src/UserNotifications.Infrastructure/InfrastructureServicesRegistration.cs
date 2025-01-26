using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications.Notification;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Domain.UpdateDetails;
using UserNotifications.Infrastructure.Database;
using UserNotifications.Infrastructure.Repositories;
using UserNotifications.Infrastructure.Services;
using UserNotifications.Infrastructure.SpecificationHandlers;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.Builders;

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

            services.AddScoped<ISpecificationHandler<Updates>, UpdateNotificationSpecificationHandler>();
            services.AddScoped<IRangeSpecificationHandler<GetRange, Notification>, GetRangeNotificationsSpecificationHandler>();

            services.AddSingleton<IMongoUpdateDifinitionService<INotificationUpdateDetails, Notification>, NotificationMongoUpdateDifinitionService>();

            services.AddSingleton<IUpdateDefinitionFactory>(new UpdateDefinitionFactory
            {
                NotificationUpdateBuilder = (x) => new NotificationMongoUpdateDifinitionBuilder(x)
            });

            return services;
        }
    }
}
