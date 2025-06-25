using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.PermissionStore.Application.Behaviors;
using MarketToolsV3.PermissionStore.Application.Services.Abstract;
using MarketToolsV3.PermissionStore.Application.Services.Implementation;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Application.Utilities.Implementation;

namespace MarketToolsV3.PermissionStore.Application
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddSingleton<IPermissionsUtility, PermissionsUtility>();
            services.AddSingleton<IPermissionNodeService, PermissionNodeService>();
            services.AddScoped<IPermissionSettingContextService, PermissionSettingContextService>();

            return services;
        }
    }
}
