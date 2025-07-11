using MarketToolsV3.ApiGateway;
using MarketToolsV3.ApiGateway.Middlewares;
using MarketToolsV3.ApiGateway.Services.Implementation;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Proto.Contract.Identity;
using Scalar.AspNetCore;
using System.IdentityModel.Tokens.Jwt;
using MarketToolsV3.ApiGateway.Domain.Seed;
using MarketToolsV3.ConfigurationManager.Extensions;

var builder = WebApplication.CreateBuilder(args);

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServicesAddressesConfig> addressesConfig = await configurationServiceFactory.CreateFromServicesAddressesAsync();
addressesConfig.AddAsOptions(builder.Services);

var module = addressesConfig.Value.GetApiGatewayModule();

ITypingConfigManager<ServiceConfiguration> serviceConfigManager = await configurationServiceFactory
    .CreateFromServiceAsync<ServiceConfiguration>(module.Name);
serviceConfigManager.JoinTo(builder.Configuration);

var authConfigManager = await configurationServiceFactory.CreateFromAuthAsync();
authConfigManager.AddAsOptions(builder.Services);

ITypingConfigManager<ServicesAddressesConfig> servicesAddressesConfigManager 
    = await configurationServiceFactory.CreateFromServicesAddressesAsync();

var ocelotPipelineConfiguration = OcelotPipelineConfigurationFactory.Create();

builder.Services
    .AddAuthGrpcClient(servicesAddressesConfigManager.Value)
    .AddApiGatewayServices(serviceConfigManager.Value)
    .AddServiceAuthentication(authConfigManager.Value);

builder.Services
    .AddOcelot(builder.Configuration);

builder.AddServiceDefaults();

string corsName = builder.Services
    .AddDevCorsServices();

var app = builder.Build();

app.UseCors(corsName);

app.UseAuthentication();

app.UseMiddleware<AccessTokenContextMiddleware>();
app.UseMiddleware<AccessTokenBlackListMiddleware>();
app.UseMiddleware<ModuleCheckMiddleware>();

await app.UseOcelot(ocelotPipelineConfiguration);

app.Run();
