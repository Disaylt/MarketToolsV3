using Identity.Application;
using Identity.Domain.Constants;
using Identity.Domain.Seed;
using Identity.GrpcService;
using Identity.GrpcService.Services;
using Identity.Infrastructure;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(IdentityConfig.ServiceName);
serviceConfigManager.AddAsOptions(builder.Services);
ITypingConfigManager<AuthConfig> authConfigManager =
    await configurationServiceFactory.CreateFromAuthAsync();
authConfigManager.AddAsOptions(builder.Services);
ITypingConfigManager<MessageBrokerConfig> messageBrokerConfigManager =
    await configurationServiceFactory.CreateFromMessageBrokerAsync();

builder.Services.AddGrpc();

builder.Services.AddServiceAuthentication(authConfigManager.Value);

builder.Services
    .AddMessageBroker(messageBrokerConfigManager.Value)
    .AddInfrastructureLayer(serviceConfigManager.Value)
    .AddApplicationLayer();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGrpcService<AuthService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
