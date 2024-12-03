using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Processor
{
    internal static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection collection, MessageBrokerConfig messageBrokerConfig)
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

                    cfg.ConfigureEndpoints(context);
                });
            });

            return collection;
        }
    }
}
