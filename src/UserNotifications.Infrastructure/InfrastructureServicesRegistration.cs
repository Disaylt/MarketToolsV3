using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications.Notifications.GetRangeByDateUserAndLimit;
using UserNotifications.Applications.Specifications.Notifications.UpdateIsReadByRange;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Database;
using UserNotifications.Infrastructure.Repositories;
using UserNotifications.Infrastructure.Services.Abstract;
using UserNotifications.Infrastructure.Services.Implementation;
using UserNotifications.Infrastructure.SpecificationHandlers.Notifications.GetRangeByDateUserAndLimit;
using UserNotifications.Infrastructure.SpecificationHandlers.Notifications.UpdateIsReadByRange;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.Builders;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.NewFieldsData;

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

            services.AddScoped<ISpecificationHandler<UpdateIsReadByRangeFilterNotificationSpecififcation>,
                UpdateIsReadByRangeFilterNotificationSpecififcationHandler>();

            services.AddScoped<IRangeSpecificationHandler <GetRangeByDateUserAndLimitNotificationSpecification, Notification>,
                GetRangeByDateUserAndLimitNotificationSpecificationHandler>();

            services.AddSingleton<IMongoUpdateDifinitionService<INotificationNewFieldsData, Notification>, NotificationMongoUpdateDifinitionService>();

            services.AddSingleton<IUpdateDefinitionFactory>(new UpdateDefinitionFactory
            {
                NotificationUpdateBuilder = (x) => new NotificationMongoUpdateDifinitionBuilder(x)
            });

            return services;
        }
    }
}
