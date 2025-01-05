using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using UserNotifications.Applications;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure;
using UserNotifications.Processor;

string serviceName = "user-notifications";
var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
ITypingConfigManager<ServiceConfiguration> serviceConfigManager = configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(serviceName);
ITypingConfigManager<MessageBrokerConfig> messageBrokerConfigManager =
    configurationServiceFactory.CreateFromMessageBroker();

builder.Services
    .AddMessageBroker(messageBrokerConfigManager.Value, serviceName)
    .AddApplicationLayer()
    .AddInfrastructureServices(serviceConfigManager.Value);

var host = builder.Build();
host.Run();
