using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Extensions;
using MarketToolsV3.ConfigurationManager.Models;
using WB.Seller.Companies.Application;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure;
using WB.Seller.Companies.Processor;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServicesAddressesConfig> addressesConfig = await configurationServiceFactory.CreateFromServicesAddressesAsync();
addressesConfig.AddAsOptions(builder.Services);

var module = addressesConfig.Value.GetWbSellerCompaniesModule();

ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(module.Name);
ITypingConfigManager<MessageBrokerConfig> messageBrokerConfigManager =
    await configurationServiceFactory.CreateFromMessageBrokerAsync();

builder.Services
    .AddMessageBroker(messageBrokerConfigManager.Value, module.Name)
    .AddApplicationServices()
    .AddInfrastructureLayer(serviceConfigManager.Value);

var host = builder.Build();
host.Run();