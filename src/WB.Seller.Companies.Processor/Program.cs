using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Extensions;
using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using WB.Seller.Companies.Application;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure;
using WB.Seller.Companies.Processor;
using WB.Seller.Companies.Processor.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServicesAddressesConfig> addressesConfig = await configurationServiceFactory.CreateFromServicesAddressesAsync();
addressesConfig.AddAsOptions(builder.Services);

var module = addressesConfig.Value.GetWbSellerCompaniesModule();

ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(module.Name);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureLayer(serviceConfigManager.Value);

await builder.Services.ConfigureBrokerMessenger(configurationServiceFactory,
    mt =>
    {
        mt.AddConsumer<IdentityCreatedConsumer>();
    },
    (context, cfg) =>
    {
        cfg.ReceiveEndpoint($"{module.Name}.{nameof(IdentityCreatedConsumer)}", re =>
        {
            re.ConfigureConsumer<IdentityCreatedConsumer>(context);
        });
    });

var host = builder.Build();
host.Run();