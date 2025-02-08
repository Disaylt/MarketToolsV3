using Identity.Application.Behaviors;
using Identity.Application.Commands;
using Identity.Application.Seed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Mappers.Abstract;
using Identity.Application.Mappers.Implementation;
using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.Application.Services.Implementation;
using FluentValidation;

namespace Identity.Application
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(DeepValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            serviceCollection.AddSingleton<ISessionMapper<SessionDto>, SessionDtoMapper>();

            serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            serviceCollection.AddScoped<IStringIdQuickSearchService<SessionDto>, SessionQuickSearchService>();

            serviceCollection
                .AddScoped<IDeepValidator<DeactivateSessionCommand>, DeactivateSessionCommandDeepValidate>();

            return serviceCollection;
        }
    }
}
