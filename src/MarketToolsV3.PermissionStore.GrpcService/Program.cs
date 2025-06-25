using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Extensions;
using MarketToolsV3.ConfigurationManager.Models;
using MarketToolsV3.PermissionStore.Application;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MarketToolsV3.PermissionStore.GrpcService.Services;
using MarketToolsV3.PermissionStore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServicesAddressesConfig> addressesConfig = await configurationServiceFactory.CreateFromServicesAddressesAsync();
addressesConfig.AddAsOptions(builder.Services);

var module = addressesConfig.Value.GetPermissionsModule();

ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(module.Name);

builder.AddServiceDefaults();

builder.Services.AddApplicationServices()
    .AddInfrastructureServices(serviceConfigManager.Value);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGrpcService<PermissionsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
