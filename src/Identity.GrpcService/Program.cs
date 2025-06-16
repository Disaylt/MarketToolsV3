using Identity.Application;
using Identity.Domain.Seed;
using Identity.GrpcService;
using Identity.GrpcService.Services;
using Identity.Infrastructure;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Extensions;
using MarketToolsV3.ConfigurationManager.Models;
using MarketToolV3.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
ITypingConfigManager<ServicesAddressesConfig> addressesConfig = await configurationServiceFactory.CreateFromServicesAddressesAsync();
addressesConfig.AddAsOptions(builder.Services);

var module = addressesConfig.Value.GetIdentityModule();

ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(module.Name);
serviceConfigManager.AddAsOptions(builder.Services);
ITypingConfigManager<AuthConfig> authConfigManager =
    await configurationServiceFactory.CreateFromAuthAsync();
authConfigManager.AddAsOptions(builder.Services);
ITypingConfigManager<MessageBrokerConfig> messageBrokerConfigManager =
    await configurationServiceFactory.CreateFromMessageBrokerAsync();

builder.Services.AddGrpc();

builder.Services.AddServiceAuthentication(authConfigManager.Value, false);

builder.Services
    .AddMessageBroker(messageBrokerConfigManager.Value)
    .AddInfrastructureLayer(serviceConfigManager.Value)
    .AddApplicationLayer();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGrpcService<AuthService>();
app.MapGrpcService<SessionService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
