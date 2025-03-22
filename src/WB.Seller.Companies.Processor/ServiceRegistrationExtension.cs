using MarketToolsV3.ConfigurationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace WB.Seller.Companies.Processor
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

                    cfg.ConfigureEndpoints(context);
                });
            });

            return collection;
        }
    }
}
