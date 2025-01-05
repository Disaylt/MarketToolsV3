using MarketToolsV3.ConfigurationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

string serviceName = "api-gateway";


var builder = WebApplication.CreateBuilder(args);

ConfigurationManagersFactory configurationManagersFactory = new(builder.Configuration);
configurationManagersFactory.Create()

builder.Services.AddOcelot(builder.Configuration);

builder.AddServiceDefaults();

var app = builder.Build();

app.MapDefaultEndpoints();

await app.UseOcelot();
app.Run();
