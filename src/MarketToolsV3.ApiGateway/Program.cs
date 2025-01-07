using MarketToolsV3.ApiGateway;
using MarketToolsV3.ApiGateway.Domain.Constants;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Proto.Contract.Identity;

var builder = WebApplication.CreateBuilder(args);


ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

IConfigManager serviceConfigManager = await configurationServiceFactory.CreateFromServiceAsync(ApiGatewayConfig.ServiceName);
serviceConfigManager.JoinTo(builder.Configuration);

ITypingConfigManager<ServicesAddressesConfig> servicesAddressesConfigManager 
    = await configurationServiceFactory.CreateFromServicesAddressesAsync();

builder.Services
    .AddAuthGrpcClient(servicesAddressesConfigManager.Value);


builder.Services.AddOcelot(builder.Configuration);

builder.AddServiceDefaults();

var app = builder.Build();

await app.UseOcelot();

app.Run();
