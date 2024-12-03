using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using UserNotifications.Applications;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure;

string serviceName = "user-notifications";
var builder = Host.CreateApplicationBuilder(args);

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
ITypingConfigManager<ServiceConfiguration> serviceConfigManager = configurationServiceFactory.CreateFromService<ServiceConfiguration>(serviceName);

builder.Services.AddApplicationLayer()
    .AddInfrastructureServices(serviceConfigManager.Value);

var host = builder.Build();
host.Run();
