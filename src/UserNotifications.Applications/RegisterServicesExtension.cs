using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Behaviors;
using UserNotifications.Applications.Mappers;
using UserNotifications.Applications.Models;
using UserNotifications.Applications.Services;

namespace UserNotifications.Applications
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            serviceCollection.AddMappers();

            serviceCollection.AddSingleton<INotificationFiltersService, NotificationFiltersService>();

            return serviceCollection;
        }

        private static void AddMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INotificationMapper<NotificationDto>, NotificationDtoMapper>();
        }
    }
}
