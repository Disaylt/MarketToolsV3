using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

string serviceName = "api-gateway";


var builder = WebApplication.CreateBuilder(args);

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

IConfigManager serviceConfigManager = await configurationServiceFactory.CreateFromServiceAsync(serviceName);
serviceConfigManager.JoinTo(builder.Configuration);


builder.Services.AddOcelot(builder.Configuration);

builder.AddServiceDefaults();

var app = builder.Build();

await app.UseOcelot();

app.Run();
