using Identity.Domain.Seed;
using Identity.WebApi.Services;
using Identity.WebApi.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Identity.Infrastructure.Database;
using Identity.WebApi.Services.Implementation;
using Identity.WebApi.ExceptionHandlers;
using MassTransit.Configuration;

namespace Identity.WebApi
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection collection, MessageBrokerConfig messageBrokerConfig)
        {
            collection.AddMassTransit(mt =>
            {
                mt.AddEntityFrameworkOutbox<IdentityDbContext>(o =>
                {
                    o.UsePostgres();

                    o.UseBusOutbox();
                });

                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBrokerConfig.RabbitMqConnection,
                        "/",
                        h =>
                        {
                            h.Username(messageBrokerConfig.RabbitMqLogin);
                            h.Password(messageBrokerConfig.RabbitMqPassword);
                        });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return collection;
        }

        public static void AddWebApiServices(this IServiceCollection collection)
        {
            collection.AddScoped<ISessionContextService, SessionContextService>();
            collection.AddScoped<ICookiesContextService, CookiesContextService>();
            collection.AddScoped<ICredentialsService, CredentialsService>();
            collection.AddProblemDetails();
            collection.AddExceptionHandler<RootExceptionHandler>();

            collection.AddSingleton<ISessionViewMapper, SessionViewMapper>();
        }
    }
}
