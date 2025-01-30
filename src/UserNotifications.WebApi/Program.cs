using Asp.Versioning;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using UserNotifications.Applications;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure;
using UserNotifications.Infrastructure.Database;

string serviceName = "user-notifications";
var builder = WebApplication.CreateBuilder(args);


ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
ITypingConfigManager<ServiceConfiguration> serviceConfigManager = 
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(serviceName);

MongoDatabaseBuilder test = new MongoDatabaseBuilder(serviceConfigManager.Value);
await test.Build();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi("v1");

builder.Services.AddApplicationLayer()
    .AddInfrastructureServices(serviceConfigManager.Value);

builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
