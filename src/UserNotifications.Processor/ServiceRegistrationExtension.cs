using IntegrationEvents.Contract.Identity;
using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Processor.Consumers;

namespace UserNotifications.Processor
{
    internal static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection collection, 
            MessageBrokerConfig messageBrokerConfig,
            string serviceName)
        {
            collection.AddMassTransit(mt =>
            {
                mt.AddConsumer<IdentityCreatedConsumer>();
                mt.AddConsumer<SessionCreatedConsumer>();

                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBrokerConfig.RabbitMqConnection,
                        "/",
                        h =>
                        {
                            h.Username(messageBrokerConfig.RabbitMqLogin);
                            h.Password(messageBrokerConfig.RabbitMqPassword);
                        });

                    cfg.ReceiveEndpoint($"{serviceName}.{nameof(IdentityCreatedConsumer)}", re =>
                    {
                        re.ConfigureConsumer<IdentityCreatedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint($"{serviceName}.{nameof(SessionCreatedConsumer)}", re =>
                    {
                        re.ConfigureConsumer<SessionCreatedConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return collection;
        }
    }
}
