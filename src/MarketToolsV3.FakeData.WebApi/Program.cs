using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.FakeData.WebApi.Application;
using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Extensions;
using MarketToolsV3.FakeData.WebApi.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServiceConfig> serviceConfigManager =
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfig>(FakeDataConfig.ServiceName);
serviceConfigManager.AddAsOptions(builder.Services);

builder.AddServiceDefaults();

builder.Services
    .AddInfrastructureService(serviceConfigManager.Value)
    .AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddOpenApi("v1");

var app = builder.Build();

app.Subscribe<TimeoutTaskDetailsNotification>()
    .Subscribe<FakeDataTaskNotification>();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
