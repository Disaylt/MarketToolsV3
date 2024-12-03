using IntegrationEvents.Contract.Identity;
using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
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
                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBrokerConfig.RabbitMqConnection,
                        "/",
                        h =>
                        {
                            h.Username(messageBrokerConfig.RabbitMqLogin);
                            h.Password(messageBrokerConfig.RabbitMqPassword);
                        });

                    cfg.ReceiveEndpoint($"{serviceName}.IdentityCreatedQueue", re =>
                    {
                        re.Consumer<IdentityCreatedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint($"{serviceName}.SessionCreatedQueue", re =>
                    {
                        re.Consumer<SessionCreatedConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return collection;
        }
    }
}
