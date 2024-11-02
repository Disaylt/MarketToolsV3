using Identity.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            return serviceCollection;
        }
    }
}
