using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using UserNotifications.Applications;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure;
using UserNotifications.Processor;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
ITypingConfigManager<ServicesAddressesConfig> addressesConfig = await configurationServiceFactory.CreateFromServicesAddressesAsync();
addressesConfig.AddAsOptions(builder.Services);

var module = addressesConfig.Value.UserNotifications;

ITypingConfigManager<ServiceConfiguration> serviceConfigManager = 
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(module.Name);
ITypingConfigManager<MessageBrokerConfig> messageBrokerConfigManager =
    await configurationServiceFactory.CreateFromMessageBrokerAsync();

builder.Services
    .AddMessageBroker(messageBrokerConfigManager.Value, module.Name)
    .AddApplicationLayer()
    .AddInfrastructureServices(serviceConfigManager.Value);

var host = builder.Build();
host.Run();
