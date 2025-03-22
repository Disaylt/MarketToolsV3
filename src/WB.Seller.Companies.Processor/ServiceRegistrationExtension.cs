using MarketToolsV3.ConfigurationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using WB.Seller.Companies.Processor.Consumers;

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

                mt.AddConsumer<IdentityCreatedConsumer>();

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

                    cfg.ConfigureEndpoints(context);
                });
            });

            return collection;
        }
    }
}
